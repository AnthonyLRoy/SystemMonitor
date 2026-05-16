// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetReadGroupsController.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The get read groups controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Controllers
{
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
    /// The get read groups controller.
    /// </summary>
    public class GetReadGroupsController : BaseController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get groups.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]
        public HttpResponseMessage GetGroups([FromUri] string filter)
        {
            var parameterCollection = new ParameterCollection(new FilterValidator(), new FilterTranslator())
                                          {
                                              {
                                                  "filter", 
                                                  new Parameter
                                                  <
                                                  string, 
                                                  string
                                                  >(
                                                  "@filter", 
                                                  filter)
                                              }
                                          };
            parameterCollection.ValidateAndTranslate();
            if (parameterCollection.IsTranslated && parameterCollection.IsValidated)
            {
                var results = this.TheStatisticsRepository.GetData<FilterCommand, RepositoryFilter>(parameterCollection);
                var stats = this.TheModelFactory.Convert(results, new FilterToFilterResultsConverter()).ToList();
                this.Response = this.Request.CreateResponse(HttpStatusCode.OK, stats);
            }

            return this.Response;
        }

        #endregion
    }
}