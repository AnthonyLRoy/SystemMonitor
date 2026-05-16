// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceMonitorManager.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The performance monitor manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Client.Monitor.Reader.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Autofac;

    using Castle.Core.Internal;

    using Dan.Common.Messages;
    using Dan.Common.Messages.Helpers;
    using Dan.monitor.Common;
    using Dan.monitor.Common.Dan.Common.Messages;

    using log4net;

    using Configuration = SimpleConfigSections.Configuration;

    /// <summary>
    /// The performance monitor manager.
    /// </summary>
    public class PerformanceMonitorManager
    {
        #region Static Fields

        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Fields

        /// <summary>
        /// The _metric readers.
        /// </summary>
        private readonly List<IMetricReader> _metricReaders = new List<IMetricReader>();

        /// <summary>
        /// The _tasks.
        /// </summary>
        private readonly List<Task> _tasks = new List<Task>();

        /// <summary>
        /// The _timers.
        /// </summary>
        private readonly List<Timer> _timers = new List<Timer>();

        /// <summary>
        /// The _wtoken.
        /// </summary>
        private readonly CancellationTokenSource _wtoken;

        /// <summary>
        /// The _message manager.
        /// </summary>
        private IMessageManager _messageManager;

        /// <summary>
        /// The _message publishing frequency.
        /// </summary>
        private int _messagePublishingFrequency = 30000;

        /// <summary>
        /// The sequence.
        /// </summary>
        private int sequence = 0;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceMonitorManager"/> class.
        /// </summary>
        public PerformanceMonitorManager()
        {
            this.Initialise();
            this._wtoken = new CancellationTokenSource();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The start.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        internal bool Start()
        {
            Log.Info("Starting Readers");
            foreach (var metricreader in this._metricReaders)
            {
                metricreader.Start(this._wtoken);
            }

            Log.Info(
                string.Format(
                    "Sending Messages to ServiceBus every {0} seconds", 
                    this._messagePublishingFrequency / 1000));
            this._timers.Add(new Timer(this.SendMessages, null, this._messagePublishingFrequency, Timeout.Infinite));
            return true;
        }

        /// <summary>
        /// The stop.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        internal bool Stop()
        {
            this._wtoken.Cancel();
            try
            {
                foreach (var task in this._tasks)
                {
                    task.Wait();
                }
            }
            catch (AggregateException)
            {
            }

            return true;
        }

        /// <summary>
        /// The get list of libraries.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<object> GetListOfLibraries()
        {
            var types =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(
                        x =>
                        x.GetTypes()
                            .Where(
                                t =>
                                t.GetCustomAttribute<MetricReaderAttribute>() != null
                                && t.GetCustomAttribute<MetricReaderAttribute>().IsActive
                                && typeof(IMetricReader).IsAssignableFrom(t)));
            return types;
        }

        /// <summary>
        /// The initialise.
        /// </summary>
        private void Initialise()
        {
            this.LoadDefaults();
            this.LoadExternalAssemblies();
            this.InitialiseReaders();
        }

        /// <summary>
        /// The initialise readers.
        /// </summary>
        private void InitialiseReaders()
        {
            Log.Info("Initialising Readers");
            foreach (var metricReaderType in this.GetListOfLibraries())
            {
                this._metricReaders.Add(Activator.CreateInstance((Type)metricReaderType) as IMetricReader);
            }

            Log.Info("Loaded Readers");
        }

        /// <summary>
        /// The load defaults.
        /// </summary>
        private void LoadDefaults()
        {
            this._messageManager = autofac.Container.Resolve<IMessageManager>();
            if (
                !int.TryParse(
                    ConfigurationManager.AppSettings["SB_PUBLISH_FREQUENCY"], 
                    out this._messagePublishingFrequency))
            {
                this._messagePublishingFrequency = 30000;
            }
        }

        /// <summary>
        /// The load external assemblies.
        /// </summary>
        private void LoadExternalAssemblies()
        {
            Log.Info("loading External Assemblies");
            Log.Info("Loading assemblies");
            var config = Configuration.Get<IExternalReaderLibrary>();
            foreach (var library in config.libraries)
            {
                Log.Info(string.Concat("Found assembly :", library.name));
                Assembly.LoadFile(
                    string.Concat(
                        library.location.IsNullOrEmpty() ? AppDomain.CurrentDomain.BaseDirectory : library.location, 
                        @"\", 
                        library.name));
                Log.Info(string.Concat("loaded assembly :", library.name));
            }
        }

        /// <summary>
        /// The send messages.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        private void SendMessages(object state)
        {
            var values = new List<IMetricValue>();
            Log.Info("Preparing to Send Message");
            Log.Info("Collating Events");
            foreach (var metricreader in this._metricReaders.Where(metricreader => metricreader != null))
            {
                values.AddRange(metricreader.Results());
            }

            var monitorMessageCollection = new MonitorMessageCollection(
                new CustomXmlSerializer(), 
                Environment.UserDomainName);
            foreach (var metricValue in values)
            {
                var message = new MonitorMessage(new CustomXmlSerializer());
                message.Properties.Add("ID", metricValue.Id);
                message.Properties.Add("TypeCode", metricValue.TypeCode);
                message.Properties.Add("TextMessage", metricValue.TextMessage);
                message.Properties.Add("rawvalue", metricValue.Raw.ToString(CultureInfo.InvariantCulture));
                message.Properties.Add("calculated", metricValue.Calculated.ToString(CultureInfo.InvariantCulture));
                message.Properties.Add("created", metricValue.Created.ToUniversalTime());
                monitorMessageCollection.Messages.Add(message);
            }

            Log.Info(string.Format("Collated {0} Events", monitorMessageCollection.Messages.Count));
            Log.Info("Attempting to send Message");
            this._messageManager.SendMessages(monitorMessageCollection);
            Log.Info("Sent message successfully");
            Log.Info(
                string.Format(
                    "Resetting send message timer. Next message in {0} seconds", 
                    this._messagePublishingFrequency / 1000));
            this._timers.Add(new Timer(this.SendMessages, null, this._messagePublishingFrequency, Timeout.Infinite));
        }

        #endregion
    }
}