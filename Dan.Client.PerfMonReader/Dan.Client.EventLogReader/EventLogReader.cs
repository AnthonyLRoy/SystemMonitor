// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventLogReader.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The performance monitor reader.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Client.EventLogReader
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
    public class EventLogReaderService : IMetricReader
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
        private List<EventLogTask> _performanceObjects;

        /// <summary>
        /// The _schedule.
        /// </summary>
        private Timer _schedule;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLogReaderService"/> class. 
        /// Initializes a new instance of the <see cref="SqlReader"/> class.
        /// </summary>
        public EventLogReaderService()
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
                this._currentMetrics.Clear();
                var tasks = this._performanceObjects.Select(this.ReadCounterAsync).ToList();
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
                            if (taskResult.Result.Id != string.Empty)
                            {
                                this._currentMetrics.Add(taskResult.Result.Id, taskResult.Result);
                            }
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
        private async Task<MetricValue> ReadCounterAsync(EventLogTask counterTask)
        {
            // Read the directory information from 
            eventlogsEventlog counter = counterTask.Data;
            if (!counterTask.ReadyToExecute())
            {
                return new MetricValue(0, 0, "notProcessed", DateTime.Now);
            }

            try
            {
                List<EventLogEntry> result = null;
                var el = new EventLog();
                el.Log = counterTask.Data.eventlogname;
                el.MachineName = counterTask.Data.machinename;

                try
                {
                    result = (from EventLogEntry elog in el.Entries
                              where
                                  elog.TimeGenerated >= counterTask.LastExecuted
                                  && (elog.Source == counterTask.Data.source)
                                  && (elog.EntryType.ToString() == counterTask.Data.level.ToString())
                              orderby elog.TimeGenerated descending
                              select elog).ToList();
                    Console.WriteLine("found {0} Event log Entries", result.Count);

                    // selrilize the Eventlog
                    var logsResult = new EventLogs();
                    logsResult.SetResults(result);
                    counterTask.LastExecuted = DateTime.Now;
                    if (result.Count != 0)
                    {
                        var serial = Dan.monitor.Common.Helpers.XmlHelper.SerializeObject(logsResult);
                        return new MetricValue(2, serial, counterTask.Data.name);
                    }
                }
                catch (InvalidOperationException t)
                {
                    Log.Info("No new log entries");
                }

                return new MetricValue(2, string.Empty, string.Empty);
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
        private IEnumerable<EventLogTask> SetUpCounters()
        {
            var serializer = new XmlSerializer(typeof(eventlogs));

            using (var stream = File.OpenRead("EventlogConfig.XML"))
            {
                Log.Info("Attempting to access Event log Configuration files ");
                return
                    ((eventlogs)serializer.Deserialize(stream)).eventlog.Select(
                        eventTask => new EventLogTask(eventTask)).ToList();
            }
        }

        #endregion
    }
}