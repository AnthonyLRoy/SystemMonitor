// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMetricReader.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The MetricReader interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages
{
    using System.Collections.Generic;
    using System.Threading;

    using Dan.monitor.Common.Dan.Common.Messages;

    /// <summary>
    /// The MetricReader interface.
    /// </summary>
    public interface IMetricReader
    {
        #region Public Methods and Operators

        /// <summary>
        /// The process.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        void Process(object state);

        /// <summary>
        /// The results.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<IMetricValue> Results();

        /// <summary>
        /// The start.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Start(CancellationTokenSource token);

        /// <summary>
        /// The stop.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Stop(CancellationToken token);

        #endregion
    }
}