// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseStatisticsRepository.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The base statistics repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Repositories
{
    using System.Data.SqlClient;

    /// <summary>
    /// The base statistics repository.
    /// </summary>
    public abstract class BaseStatisticsRepository
    {
        #region Fields

        /// <summary>
        /// The connection.
        /// </summary>
        protected SqlConnection Connection;

        /// <summary>
        /// The database key.
        /// </summary>
        protected string DatabaseKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseStatisticsRepository"/> class.
        /// </summary>
        /// <param name="databaseKey">
        /// The database key.
        /// </param>
        protected BaseStatisticsRepository(string databaseKey)
        {
            this.DatabaseKey = databaseKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseStatisticsRepository"/> class.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        protected BaseStatisticsRepository(SqlConnection connection)
        {
            this.Connection = connection;
        }

        #endregion
    }
}