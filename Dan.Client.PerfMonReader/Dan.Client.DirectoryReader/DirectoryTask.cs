// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseTask.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The database task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Client.DirectoryReader
{
    using System;

    /// <summary>
    /// The database task.
    /// </summary>
    public class DirectoryTask
    {
        #region Fields

        /// <summary>
        /// The last executed.
        /// </summary>
        public DateTime LastExecuted;

        /// <summary>
        /// The _data.
        /// </summary>
        private readonly directoriesDirectory _data;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryTask"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public DirectoryTask(directoriesDirectory data)
        {
            this._data = data;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the data.
        /// </summary>
        public directoriesDirectory Data
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
                case directoriesDirectoryCounterScheduleFrequency.H:
                    result = this.LastExecuted.AddHours((double)this._data.counter.schedule.frequency) < currentdate;
                    break;

                case directoriesDirectoryCounterScheduleFrequency.M:
                    result = this.LastExecuted.AddMinutes((double)this._data.counter.schedule.frequency) < currentdate;
                    break;
                case directoriesDirectoryCounterScheduleFrequency.S:
                    result = this.LastExecuted.AddSeconds((double)this._data.counter.schedule.frequency) < currentdate;
                    break;
            }

            return result;
        }

        #endregion
    }
}