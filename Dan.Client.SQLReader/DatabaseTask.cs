// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseTask.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The database task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Client.SQLReader
{
    using System;

    /// <summary>
    /// The database task.
    /// </summary>
    public class DatabaseTask
    {
        #region Fields

        /// <summary>
        /// The last executed.
        /// </summary>
        public DateTime LastExecuted;

        /// <summary>
        /// The _data.
        /// </summary>
        private readonly databasesDatabase _data;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTask"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public DatabaseTask(databasesDatabase data)
        {
            this._data = data;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the data.
        /// </summary>
        public databasesDatabase Data
        {
            get
            {
                return this._data;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The ready to execute.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ReadyToExecute()
        {
            bool result = false;
            DateTime currentdate = DateTime.Now;

            if ((this._data.counter.schedule.daypattern.Substring(0 + (int)currentdate.DayOfWeek, 1) != "1")
                || (currentdate.TimeOfDay <= this._data.counter.schedule.start.TimeOfDay
                    || currentdate.TimeOfDay >= this._data.counter.schedule.end.TimeOfDay))
            {
                return false;
            }

            switch (this._data.counter.schedule.frequency)
            {
                case databasesDatabaseCounterScheduleFrequency.H:
                    result = this.LastExecuted.AddHours((double)this._data.counter.schedule.frequency) < currentdate;
                    break;

                case databasesDatabaseCounterScheduleFrequency.M:
                    result = this.LastExecuted.AddMinutes((double)this._data.counter.schedule.frequency) < currentdate;
                    break;
                case databasesDatabaseCounterScheduleFrequency.S:
                    result = this.LastExecuted.AddSeconds((double)this._data.counter.schedule.frequency) < currentdate;
                    break;
            }

            return result;
        }

        #endregion
    }
}