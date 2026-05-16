// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceMonitorReader.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The performance monitor reader.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Client.PerfMonReader
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using Dan.Common.Messages;
    using Dan.Common.Messages.Performance;
    using Dan.monitor.Common;
    using Dan.monitor.Common.Dan.Common.Messages;

    using log4net;

    /// <summary>
    /// The performance monitor reader.
    /// </summary>
    [MetricReader(true)]
    public class PerformanceMonitorReader : IMetricReader
    {
        #region Static Fields

        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Fields

        /// <summary>
        /// The _current metrics.
        /// </summary>
        private readonly Dictionary<string, MetricValue> _currentMetrics = new Dictionary<string, MetricValue>();

        /// <summary>
        /// The _performance objects.
        /// </summary>
        private List<PerfCounter> _performanceObjects;

        /// <summary>
        /// The _schedule.
        /// </summary>
        private Timer _schedule;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceMonitorReader"/> class.
        /// </summary>
        public PerformanceMonitorReader()
        {
            this._schedule = null;
            this.InitializeComponent();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The initialize component.
        /// </summary>
        public void InitializeComponent()
        {
            Log.Info("Preparing Counters");
            this._performanceObjects = this.SetUpCounters().Where(perfCounter => perfCounter.Pcounter != null).ToList();
        }

        /// <summary>
        /// The process.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        public async void Process(object state)
        {
            try
            {
                Log.Info("processing Counters");
                List<Task<MetricValue>> tasks = this._performanceObjects.Select(this.ReadCounterAsync).ToList();
                Log.Info(string.Format("Found {0} Counters to read", tasks.Count));
                while (tasks.Count > 0)
                {
                    var taskResult = await Task.WhenAny(tasks.ToArray());
                    tasks.Remove(taskResult);
                    if (this._currentMetrics.ContainsKey(taskResult.Result.Id))
                    {
                        this._currentMetrics[taskResult.Result.Id] = taskResult.Result;
                    }
                    else
                    {
                        this._currentMetrics.Add(taskResult.Result.Id, taskResult.Result);
                    }
                }

                Log.Info("Schedule Next read in 10 seconds");
                this._schedule.Change(10000, Timeout.Infinite);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception" + e.Message);
                throw;
            }
        }

        /// <summary>
        /// The results.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<IMetricValue> Results()
        {
            return this._currentMetrics.Values.ToList();
        }

        /// <summary>
        /// The start.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Start(CancellationTokenSource token)
        {
            this._schedule = new Timer(this.Process, null, 10000, Timeout.Infinite);
            return true;
        }

        /// <summary>
        /// The stop.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Stop(CancellationToken token)
        {
            this._schedule.Change(Timeout.Infinite, Timeout.Infinite);
            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The generate unique name for counter.
        /// </summary>
        /// <param name="counters">
        /// The counters.
        /// </param>
        private void GenerateUniqueNameForCounter(IEnumerable<PerfCounter> counters)
        {
            foreach (var perfCounter in counters)
            {
                perfCounter.Instance = string.Format(
                    "{0}_{1}_{2}_{3}_{4}", 
                    Environment.MachineName, 
                    perfCounter.Applicationname, 
                    perfCounter.Category, 
                    perfCounter.Counter, 
                    perfCounter.InstanceName
                    );
            }
        }

        /// <summary>
        /// The read counter async.
        /// </summary>
        /// <param name="counter">
        /// The counter.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<MetricValue> ReadCounterAsync(PerfCounter counter)
        {
            counter.Value = counter.Pcounter.NextValue();
            await Task.Delay(1000);
            counter.Value = counter.Pcounter.NextValue();
            return new MetricValue(counter.Value, counter.Value, counter.Instance, DateTime.Now);
        }

        /// <summary>
        /// The set up counters.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<PerfCounter> SetUpCounters()
        {
            var serializer = new XmlSerializer(typeof(List<PerfCounter>));
            List<PerfCounter> counters;
            // Read the Performance counter values from file
            using (var stream = File.OpenRead(ConfigurationManager.AppSettings["MONITOR_CONFIG_FILE"]))
            {
                counters = (List<PerfCounter>)serializer.Deserialize(stream);
                this.GenerateUniqueNameForCounter(counters);
                Log.Info("Attempting to access Active Counters");
                foreach (var c in counters)
                {
                    try
                    {
                        c.Pcounter = new PerformanceCounter(c.Category, c.Counter, c.InstanceName, true);
                    }
                    catch (Exception)
                    {
                        c.Pcounter = null;
                        Log.Warn(
                            string.Format(
                                "Performance Counter not Active {0} - {1}  - {2}", 
                                c.Category, 
                                c.Counter, 
                                c.InstanceName));
                    }
                }
            }

            return counters;
        }

        #endregion
    }
}