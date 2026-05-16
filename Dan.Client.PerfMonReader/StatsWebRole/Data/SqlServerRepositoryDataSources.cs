// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlServerRepositoryDataSources.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The sql server repository data sources.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    /// <summary>
    /// The sql server repository data sources.
    /// </summary>
    public static class SqlServerRepositoryDataSources
    {
        #region Static Fields

        /// <summary>
        /// The _data source connections.
        /// </summary>
        private static Dictionary<string, string> _dataSourceConnections;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the data connections.
        /// </summary>
        public static Dictionary<string, string> DataConnections
        {
            get
            {
                return _dataSourceConnections;
            }

            set
            {
                _dataSourceConnections = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get connection.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="SqlConnection"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public static SqlConnection GetConnection(string key)
        {
            try
            {
                return new SqlConnection(DataConnections[key]);
            }
            catch (Exception e)
            {
                throw new Exception("Invalid Connection Key", e);
            }
        }

        /// <summary>
        /// Read existing database connections from the existing web config Settings and
        /// add them to the the _DataSourceConnections; 
        /// </summary>
        public static void Init()
        {
            _dataSourceConnections = new Dictionary<string, string>();
            foreach (ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings)
            {
                DataConnections.Add(connectionString.Name, connectionString.ConnectionString);
            }
        }

        #endregion
    }
}