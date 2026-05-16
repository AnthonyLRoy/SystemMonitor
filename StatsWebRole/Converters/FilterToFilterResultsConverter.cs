// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterToFilterResultsConverter.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The filter to filter results converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Converters
{
    using System.Collections.Generic;
    using System.Linq;

    using StatsWebRole.Data.RepositoryModels;
    using StatsWebRole.Models;

    /// <summary>
    /// The filter to filter results converter.
    /// </summary>
    public class FilterToFilterResultsConverter : IConverter<RepositoryFilter, FilterModel>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The convert class.
        /// </summary>
        /// <param name="filterusage">
        /// The filterusage.
        /// </param>
        /// <returns>
        /// The <see cref="FilterModel"/>.
        /// </returns>
        public FilterModel ConvertClass(RepositoryFilter filterusage)
        {
            var filterRow = new FilterModel { GroupName = filterusage.GroupName };
            return filterRow;
        }

        /// <summary>
        /// The convert enumerable.
        /// </summary>
        /// <param name="eventUsageList">
        /// The event usage list.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<FilterModel> ConvertEnumerable(IEnumerable<RepositoryFilter> eventUsageList)
        {
            return from p in eventUsageList select new FilterModel { GroupName = p.GroupName, };
        }

        #endregion
    }
}