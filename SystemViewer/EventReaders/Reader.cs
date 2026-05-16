// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reader.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The reader.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.EventReaders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dan.monitor.Common;

    /// <summary>
    /// The reader.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="TOut">
    /// </typeparam>
    public class Reader<T, TOut>
    {
        #region Fields

        /// <summary>
        /// The _reader.
        /// </summary>
        private readonly IWebMetricsReader<TOut> _reader;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Reader{T,TOut}"/> class.
        /// </summary>
        /// <param name="eventMetricsReader">
        /// The event metrics reader.
        /// </param>
        public Reader(IWebMetricsReader<TOut> eventMetricsReader)
        {
            this._reader = eventMetricsReader;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="eventIds">
        /// The event ids.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<List<TOut>> GetAsync(List<string> eventIds)
        {
            var result = await this._reader.GetMetricsAsync(eventIds);
            return result;
        }

        #endregion
    }
}