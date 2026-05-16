// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceMonitorReader.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The performance monitor reader.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Client.SQLReader
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
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
    public class SqlReader : IMetricReader
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
        private List<DatabaseTask> _performanceObjects;

        /// <summary>
        /// The _schedule.
        /// </summary>
        private Timer _schedule;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlReader"/> class.
        /// </summary>
        public SqlReader()
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
                Log.Info(string.Format("Found {0} databases to read", tasks.Count));
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
        /// <param name="databaseTask">
        /// The counter.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<MetricValue> ReadCounterAsync(DatabaseTask databaseTask)
        {
            int result;
            if (databaseTask.ReadyToExecute())
            {
                using (var con = new SqlConnection(databaseTask.Data.counter.connectionstring))
                {
                    try
                    {
                        var cmd = new SqlCommand
                                      {
                                          CommandType = CommandType.Text,
                                          Connection = con,
                                          CommandText = databaseTask.Data.counter.query
                                      };
                        con.Open();
                        result = (int)await cmd.ExecuteScalarAsync();
                        databaseTask.LastExecuted = DateTime.Now;
                        return new MetricValue(result, result, databaseTask.Data.counter.countername, DateTime.Now);
                    }
                    catch (Exception e)
                    {
                        
                        throw e;
                    }
                }
              
            }

            return new MetricValue(0, 0, "notProcessed", DateTime.Now);
        }

        /// <summary>
        /// The set up counters.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<DatabaseTask> SetUpCounters()
        {
            var serializer = new XmlSerializer(typeof(databases));
           
            using (var stream = File.OpenRead("SQLConfig.XML"))
            {
                Log.Info("Attempting to access Active Counters");
               return ((databases)serializer.Deserialize(stream)).database.Select(datatask => new DatabaseTask(datatask)).ToList();
            }

            return null;
        }

        #endregion
    }
}