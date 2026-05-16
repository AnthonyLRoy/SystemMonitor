// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseTask.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The database task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Client.PerformanceMonReader
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    ///     The database task.
    /// </summary>
    public class PerfCounterTask
    {
        #region Fields

        /// <summary>
        ///     The last executed.
        /// </summary>
        public DateTime LastExecuted;

        /// <summary>
        ///     The _data.
        /// </summary>
        private readonly perfcountersPerfcounter _data;

        private PerformanceCounter counter;

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PerfCounterTask" /> class.
        /// </summary>
        /// <param name="data">
        ///     The data.
        /// </param>
        public PerfCounterTask(perfcountersPerfcounter data)
        {
            this._data = data;
            this.InitCounter();
        }

        #endregion

        #region Public Properties

        public PerformanceCounter Counter
        {
            get
            {
                return this.counter;
            }
        }

        /// <summary>
        ///     Gets the data.
        /// </summary>
        public perfcountersPerfcounter Data
        {
            get
            {
                return this._data;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.counter != null;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The ready to execute.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool ReadyToExecute()
        {
            var result = false;
            var currentdate = DateTime.Now;

            if ((this._data.schedule.daypattern.Substring(0 + (int)currentdate.DayOfWeek, 1) != "1")
                || (currentdate.TimeOfDay <= this._data.schedule.start.TimeOfDay
                    || currentdate.TimeOfDay >= this._data.schedule.end.TimeOfDay))
            {
                return false;
            }

            switch (this._data.schedule.frequency)
            {
                case perfcountersPerfcounterScheduleFrequency.H:
                    result = this.LastExecuted.AddHours((double)this._data.schedule.frequency) < currentdate;
                    break;
                case perfcountersPerfcounterScheduleFrequency.M:
                    result = this.LastExecuted.AddMinutes((double)this._data.schedule.frequency) < currentdate;
                    break;
                case perfcountersPerfcounterScheduleFrequency.S:
                    result = this.LastExecuted.AddSeconds((double)this._data.schedule.frequency) < currentdate;
                    break;
            }

            return result;
        }

        #endregion

        #region Methods

        internal void ActivateCounter()
        {
            this.InitCounter();
        }

        private void InitCounter()
        {
            try
            {
                this.counter = new PerformanceCounter(
                    this._data.category,
                    this._data.countername,
                    this._data.instance,
                    true);
            }
            catch (Exception e)
            {
                if (e is Win32Exception || e is UnauthorizedAccessException)
                {
                    this.counter = null;
                    Log.Warn(string.Format("The Counter [{0}] is not currently available.",this.Data.countername));
                }
                // counter is not currently active
            }
        }

        #endregion
    }
}