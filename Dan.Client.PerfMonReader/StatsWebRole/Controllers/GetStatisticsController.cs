// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetStatisticsController.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The get statistics controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Controllers
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using StatsWebRole.Controllers.Helpers;
    using StatsWebRole.Converters;
    using StatsWebRole.Data.Commands;
    using StatsWebRole.Data.RepositoryModels;
    using StatsWebRole.Parameters;
    using StatsWebRole.Translators;
    using StatsWebRole.Validators;

    /// <summary>
    /// The get statistics controller.
    /// </summary>
    public class GetStatisticsController : BaseController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get stats by group id.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]
        public HttpResponseMessage GetStatsByGroupID([FromUri] string groupName)
        {
            var parameterCollection = new ParameterCollection(
                new ServiceStatsValidator(), 
                new ServiceStatisticsTranslator())
                                          {
                                              {
                                                  "groupName", 
                                                  new Parameter<string, string>("@GroupName", groupName)
                                              }, 
                                          };
            parameterCollection.ValidateAndTranslate();
            if (parameterCollection.IsTranslated && parameterCollection.IsValidated)
            {
                var results =
                    this.TheStatisticsRepository.GetData<ServiceStatistics, RepositoryServiceStatistics>(
                        parameterCollection);
                var stats =
                    this.TheModelFactory.Convert(results, new ServiceStatisticsToServiceResultsConverter()).ToList();
                this.Response = this.Request.CreateResponse(HttpStatusCode.OK, stats);
            }

            return this.Response;
        }

        /// <summary>
        /// The get stats message.
        /// </summary>
        /// <param name="requestList">
        /// The request list.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpPost]
        public HttpResponseMessage GetStatsMessage([FromBody] List<string> requestList)
        {
            var parameterCollection = new ParameterCollection(
                new ServiceStatsListValidator(), 
                new ServiceStatisticsListTranslator())
                                          {
                                              {
                                                  "requestList", 
                                                  new Parameter<List<string>, DataTable>(
                                                  "@requestList", 
                                                  requestList)
                                              }, 
                                          };
            parameterCollection.ValidateAndTranslate();
            if (parameterCollection.IsTranslated && parameterCollection.IsValidated)
            {
                var results =
                    this.TheStatisticsRepository.GetData<ServiceStatisticsByList, RepositoryServiceStatistics>(
                        parameterCollection);
                var stats =
                    this.TheModelFactory.Convert(results, new ServiceStatisticsToServiceResultsConverter()).ToList();
                this.Response = this.Request.CreateResponse(HttpStatusCode.OK, stats);
            }

            return this.Response;
        }

        #endregion
    }
}