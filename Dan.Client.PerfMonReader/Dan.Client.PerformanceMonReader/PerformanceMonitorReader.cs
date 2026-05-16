// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceMonitorReader.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The performance monitor reader.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Client.PerformanceMonReader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using Dan.Common.Messages;
    using Dan.monitor.Common;
    using Dan.monitor.Common.Dan.Common.Messages;

    using log4net;

    /// <summary>
    /// The performance monitor reader.
    /// </summary>
    [MetricReader(true)]
    public class PerformanceMonReader : IMetricReader
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
        private List<PerfCounterTask> _performanceObjects;

        /// <summary>
        /// The _schedule.
        /// </summary>
        private Timer _schedule;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlReader"/> class.
        /// </summary>
        public PerformanceMonReader()
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
            this._performanceObjects = this.SetUpCounters().ToList();
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
                var tasks = this._performanceObjects.Select(this.ReadCounterAsync).ToList();
                Log.Info(string.Format("Found {0} directories in config File to process", tasks.Count));
                while (tasks.Count > 0)
                {
                    var taskResult = await Task.WhenAny(tasks.ToArray());
                    tasks.Remove(taskResult);
                    if (taskResult.Result.Id != null)
                    {
                        if (this._currentMetrics.ContainsKey(taskResult.Result.Id))
                        {
                            this._currentMetrics[taskResult.Result.Id] = taskResult.Result;
                        }
                        else
                        {
                            this._currentMetrics.Add(taskResult.Result.Id, taskResult.Result);
                        }
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
        /// The read counter async.
        /// </summary>
        /// <param name="counterTask">
        /// The counter.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<MetricValue> ReadCounterAsync(PerfCounterTask counterTask)
        {
            // Read the directory information from 
            perfcountersPerfcounter counter = counterTask.Data;
            if (!counterTask.ReadyToExecute())
            {
                return new MetricValue(0, 0, "notProcessed", DateTime.Now);
            }
            try
            {
                if (counterTask.Counter == null)
                {
                    //attempt to reconnect 
                    counterTask.ActivateCounter();
                }

                MetricValue returnValue;
                if (counterTask.IsActive)
                {
                    double result = counterTask.Counter.NextValue();
                    await Task.Delay(1000);
                    result = counterTask.Counter.NextValue();
                    returnValue = new MetricValue(result, result, counterTask.Data.name, DateTime.Now);
                }
                else
                {
                    returnValue = new MetricValue(-1 , -1 , counterTask.Data.name ,  DateTime.Now);
                }

                counterTask.LastExecuted = DateTime.Now;
                return returnValue;
            }
            catch (Exception e)
            {
                throw new Exception("Invalid Task", e);
            }
        }

        /// <summary>
        /// The set up counters.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<PerfCounterTask> SetUpCounters()
        {
            var serializer = new XmlSerializer(typeof(perfcounters));

            using (var stream = File.OpenRead("PerformanceConfig.XML"))
            {
                Log.Info("Attempting to access Active Performance Counters Counters");
                return ((perfcounters)serializer.Deserialize(stream)).perfcounter.Select(datatask => new PerfCounterTask(datatask)).ToList();
            }

            return null;
        }

        #endregion
    }
}