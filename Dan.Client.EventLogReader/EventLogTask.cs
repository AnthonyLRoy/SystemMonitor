// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseTask.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The database task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Client.EventLogReader
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    ///     The database task.
    /// </summary>
    public class EventLogTask
    {
        #region Fields

        /// <summary>
        ///     The last executed.
        /// </summary>
        public DateTime LastExecuted;

        /// <summary>
        ///     The _data.
        /// </summary>
        private readonly eventlogsEventlog _data;

        private PerformanceCounter counter;

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventLogTask" /> class.
        /// </summary>
        /// <param name="data">
        ///     The data.
        /// </param>
        public EventLogTask(eventlogsEventlog data)
        {
            this._data = data;
            this.LastExecuted = DateTime.Now;
        }

        #endregion

        #region Public Properties



        /// <summary>
        ///     Gets the data.
        /// </summary>
        public eventlogsEventlog Data
        {
            get
            {
                return this._data;
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
                case eventlogsEventlogScheduleFrequency.H:
                    result = this.LastExecuted.AddHours((double)this._data.schedule.frequency) < currentdate;
                    break;
                case eventlogsEventlogScheduleFrequency.M:
                    result = this.LastExecuted.AddMinutes((double)this._data.schedule.frequency) < currentdate;
                    break;
                case eventlogsEventlogScheduleFrequency.S:
                    result = this.LastExecuted.AddSeconds((double)this._data.schedule.frequency) < currentdate;
                    break;
            }

            return result;
        }

        #endregion

    }
}