// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventLogs.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The event logs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.monitor.Common
{
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// The event logs.
    /// </summary>
    public class EventLogs
    {
        #region Fields

        /// <summary>
        /// The _converted data.
        /// </summary>
        private List<ReducedLogEntry> _convertedData = new List<ReducedLogEntry>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the logs.
        /// </summary>
        public List<ReducedLogEntry> Logs
        {
            get
            {
                return this._convertedData;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The set results.
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        public void SetResults(List<EventLogEntry> results)
        {
            foreach (var logEntry in results)
            {
                this._convertedData.Add(new ReducedLogEntry { Message = logEntry.Message, Type = logEntry.Source });
            }
        }

        #endregion
    }
}