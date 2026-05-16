// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatisticsRepository.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The statistics repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Repositories
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using Stats.Web.Api.Data.RepoModels;

    using StatsWebRole.Data.Commands;
    using StatsWebRole.Data.Repositories;
    using StatsWebRole.Parameters.Parameter;

    /// <summary>
    /// The statistics repository.
    /// </summary>
    public class StatisticsRepository : BaseStatisticsRepository
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsRepository"/> class.
        /// </summary>
        /// <param name="databaseKey">
        /// The database key.
        /// </param>
        public StatisticsRepository(string databaseKey)
            : base(databaseKey)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsRepository"/> class.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        public StatisticsRepository(SqlConnection connection)
            : base(connection)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <typeparam name="TMethod">
        /// </typeparam>
        /// <typeparam name="TReturnClass">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<TReturnClass> GetData<TMethod, TReturnClass>(Dictionary<string, IParameter> parameters)
            where TMethod : new() where TReturnClass : BaseDataType, new()
        {
            var command = (IDataCommand)new TMethod();
            var getdata = new SqlServerDataGet<TReturnClass>(parameters, command);
            IEnumerable<TReturnClass> result = getdata.Execute();

            return result;
        }

        #endregion
    }
}