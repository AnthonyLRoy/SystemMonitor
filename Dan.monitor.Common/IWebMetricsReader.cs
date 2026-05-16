// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebMetricsReader.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The WebMetricsReader interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.monitor.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// The WebMetricsReader interface.
    /// </summary>
    /// <typeparam name="TOut">
    /// </typeparam>
    public interface IWebMetricsReader<TOut>
    {
        #region Public Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get groups async.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<List<GroupName>> GetGroupsAsync(string filter);

        /// <summary>
        /// The get metrics async.
        /// </summary>
        /// <param name="eventIds">
        /// The event ids.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<List<TOut>> GetMetricsAsync(List<string> eventIds);

        /// <summary>
        /// The get metrics by group name async.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<List<TOut>> GetMetricsByGroupNameAsync(string groupName);

        #endregion
    }
}