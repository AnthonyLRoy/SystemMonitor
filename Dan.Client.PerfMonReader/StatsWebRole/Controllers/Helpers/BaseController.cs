// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The base controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Controllers.Helpers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using StatsWebRole.Data;
    using StatsWebRole.Models;
    using StatsWebRole.Repositories;

    /// <summary>
    /// The base controller.
    /// </summary>
    public abstract class BaseController : ApiController
    {
        #region Fields

        /// <summary>
        /// The response.
        /// </summary>
        protected HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.NotFound);

        /// <summary>
        /// The _model factory.
        /// </summary>
        private ModelFactory _modelFactory;

        /// <summary>
        /// The _statistics.
        /// </summary>
        private StatisticsRepository _statistics;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        protected BaseController()
        {
            if (SqlServerRepositoryDataSources.DataConnections == null)
            {
                SqlServerRepositoryDataSources.Init();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the the model factory.
        /// </summary>
        protected ModelFactory TheModelFactory
        {
            get
            {
                return this._modelFactory ?? (this._modelFactory = new ModelFactory(this.Request));
            }

            set
            {
                this._modelFactory = value;
            }
        }

        /// <summary>
        /// Gets or sets the the statistics repository.
        /// </summary>
        protected StatisticsRepository TheStatisticsRepository
        {
            get
            {
                return this._statistics ?? (this._statistics = new StatisticsRepository("BackOffice"));
            }

            set
            {
                this._statistics = value;
            }
        }

        #endregion
    }
}