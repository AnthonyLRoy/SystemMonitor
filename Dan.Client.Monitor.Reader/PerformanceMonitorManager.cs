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
using Configuration = SimpleConfigSections.Configuration;

namespace Dan.Client.Monitor.Reader
{
    public class PerformanceMonitorManager
    {
        private readonly List<IMetricReader> _metricReaders = new List<IMetricReader>();
        private readonly List<Task> _tasks = new List<Task>();
        private readonly List<Timer> _timers = new List<Timer>();
        private readonly CancellationTokenSource _wtoken;
        private int _messagePublishingFrequency = 30000;
        private IMessageManager _messageManager;

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public PerformanceMonitorManager()
        {
            Initialise();
            _wtoken = new CancellationTokenSource();
        }

        private void Initialise()
        {
            LoadDefaults();
            LoadExternalAssemblies();
            InitialiseReaders();
        }

        private void LoadDefaults()
        {
            _messageManager = autofac.Container.Resolve<IMessageManager>();
           if (!int.TryParse(ConfigurationManager.AppSettings["SB_PUBLISH_FREQUENCy"], out _messagePublishingFrequency)) _messagePublishingFrequency = 30000;
        }

        private void LoadExternalAssemblies()
        {
            Log.Info("loading External Assemblies");
            Log.Info("Loading assemblies");
            var config = Configuration.Get<IExternalReaderLibrary>();
            foreach (ILibrary library in config.libraries)
            {
                Log.Info(string.Concat("Found assembly :",library.name));
                Assembly.LoadFile(String.Concat(
                    library.location.IsNullOrEmpty() ? AppDomain.CurrentDomain.BaseDirectory : library.location,
                    @"\", library.name));
            }
        }

        private void InitialiseReaders()
        {
            Log.Info("Initialising Readers");
            foreach (object metricReaderType in GetListOfLibraries())
            {
                _metricReaders.Add(Activator.CreateInstance((Type) metricReaderType) as IMetricReader);
            }
            Log.Info("Loaded Readers");
        }

        private IEnumerable<object> GetListOfLibraries()
        {
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany
                (x =>
                    x.GetTypes()
                        .Where(
                            t =>
                                t.GetCustomAttribute<MetricReaderAttribute>() != null &&
                                t.GetCustomAttribute<MetricReaderAttribute>().IsActive &&
                                typeof (IMetricReader).IsAssignableFrom(t)));
            return types;
        }

        internal bool Start()
        {
            Log.Info("Starting Readers");
            foreach (IMetricReader metricreader in _metricReaders) metricreader.Start(_wtoken);
            Log.Info(string.Format("Sending Messages to ServiceBus every {0} seconds", _messagePublishingFrequency));
            _timers.Add(new Timer(SendMessages, null, 30000, Timeout.Infinite));
            return true;
        }

        private void SendMessages(object state)
        {
            var values = new List<IMetricValue>();
            foreach (var metricreader in _metricReaders.Where(metricreader => metricreader != null)) values.AddRange(metricreader.Results());
            var monitorMessageCollection = new MonitorMessageCollection(new CustomXmlSerializer(), Environment.UserDomainName)
            {
                Id = Guid.NewGuid()
            };
            foreach (var metricValue in values)
            {
                var message = new MonitorMessage(new CustomXmlSerializer());
                message.Properties.Add("ID",metricValue.Id);
                message.Properties.Add("rawvalue",metricValue.Raw.ToString(CultureInfo.InvariantCulture));
                message.Properties.Add("calculated",metricValue.Calculated.ToString(CultureInfo.InvariantCulture));
                monitorMessageCollection.Messages.Add(message);
            }
            _messageManager.SendMessages(monitorMessageCollection);
            _timers.Add(new Timer(SendMessages, null, _messagePublishingFrequency, Timeout.Infinite));
        }


        internal bool Stop()
        {
            _wtoken.Cancel();
            try
            {
                foreach (Task task in _tasks)
                {
                    task.Wait();
                }
            }
            catch (AggregateException)
            {
            }
            return true;
        }
    }
}