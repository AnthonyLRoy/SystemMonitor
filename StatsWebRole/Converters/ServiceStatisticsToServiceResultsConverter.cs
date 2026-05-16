// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceStatisticsToServiceResultsConverter.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The service statistics to service results converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Converters
{
    using System.Collections.Generic;
    using System.Linq;

    using StatsWebRole.Data.RepositoryModels;
    using StatsWebRole.Models;

    /// <summary>
    /// The service statistics to service results converter.
    /// </summary>
    public class ServiceStatisticsToServiceResultsConverter : IConverter<RepositoryServiceStatistics, StatsModel>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The convert class.
        /// </summary>
        /// <param name="eventusage">
        /// The eventusage.
        /// </param>
        /// <returns>
        /// The <see cref="StatsModel"/>.
        /// </returns>
        public StatsModel ConvertClass(RepositoryServiceStatistics eventusage)
        {
            var statsrow = new StatsModel
                               {
                                   ID = eventusage.ID, 
                                   Raw = eventusage.Raw, 
                                   Calculated = eventusage.Calculated, 
                                   Created = eventusage.Created, 
                                   Received = eventusage.Received
                               };
            return statsrow;
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
        public IEnumerable<StatsModel> ConvertEnumerable(IEnumerable<RepositoryServiceStatistics> eventUsageList)
        {
            return from p in eventUsageList
                   select
                       new StatsModel
                           {
                               ID = p.ID, 
                               Raw = p.Raw, 
                               Calculated = p.Calculated, 
                               Created = p.Created, 
                               Received = p.Received
                           };
        }

        #endregion
    }
}