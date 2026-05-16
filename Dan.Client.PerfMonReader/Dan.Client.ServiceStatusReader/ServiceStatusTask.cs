// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseTask.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The database task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Client.ServiceStatusReader
{
    using System;

    /// <summary>
    /// The database task.
    /// </summary>
    public class ServiceStatusTask
    {
        #region Fields

        /// <summary>
        /// The last executed.
        /// </summary>
        public DateTime LastExecuted;

        /// <summary>
        /// The _data.
        /// </summary>
        private readonly servicesService _data;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceStatusTask"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public ServiceStatusTask(servicesService data)
        {
            this._data = data;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the data.
        /// </summary>
        public servicesService Data
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

            if ((this._data.schedule.daypattern.Substring(0 + (int)currentdate.DayOfWeek, 1) != "1")
                || (currentdate.TimeOfDay <= this._data.schedule.start.TimeOfDay
                    || currentdate.TimeOfDay >= this._data.schedule.end.TimeOfDay))
            {
                return false;
            }

            switch (this._data.schedule.frequency)
            {
                case servicesServiceScheduleFrequency.H:
                    result = this.LastExecuted.AddHours((double)this._data.schedule.frequency) < currentdate;
                    break;

                case servicesServiceScheduleFrequency.M:
                    result = this.LastExecuted.AddMinutes((double)this._data.schedule.frequency) < currentdate;
                    break;
                case servicesServiceScheduleFrequency.S:
                    result = this.LastExecuted.AddSeconds((double)this._data.schedule.frequency) < currentdate;
                    break;
            }

            return result;
        }

        #endregion
    }
}