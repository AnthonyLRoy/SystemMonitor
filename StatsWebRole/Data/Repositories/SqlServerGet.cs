// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlServerGet.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The sql server data get.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using StatsWebRole.Data.Commands;
    using StatsWebRole.Data.RepositoryModels;
    using StatsWebRole.Parameters.Parameter;

    /// <summary>
    /// The sql server data get.
    /// </summary>
    /// <typeparam name="TOut">
    /// </typeparam>
    public class SqlServerDataGet<TOut> : IDataGet<TOut>
        where TOut : new()
    {
        #region Fields

        /// <summary>
        /// The connection.
        /// </summary>
        protected SqlConnection Connection;

        /// <summary>
        /// The parameters.
        /// </summary>
        protected Dictionary<string, IParameter> Parameters;

        /// <summary>
        /// The _datacommand.
        /// </summary>
        private readonly IDataCommand _datacommand;

        /// <summary>
        /// The _parameters.
        /// </summary>
        private readonly Dictionary<string, IParameter> _parameters;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDataGet{TOut}"/> class.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public SqlServerDataGet(Dictionary<string, IParameter> parameters)
        {
            this._parameters = parameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDataGet{TOut}"/> class.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <param name="datacommand">
        /// The datacommand.
        /// </param>
        public SqlServerDataGet(Dictionary<string, IParameter> parameters, IDataCommand datacommand)
            : this(parameters)
        {
            this._datacommand = datacommand;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The execute.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public IEnumerable<TOut> Execute()
        {
            var command = new SqlCommand();
            try
            {
                if (this.Connection == null)
                {
                    this.Connection = SqlServerRepositoryDataSources.GetConnection(
                        this._datacommand.ConnectionStringKey);
                }

                command.CommandType = CommandType.StoredProcedure;
                command.Connection = this.Connection;
                this.AddParametersToCommand(command);
                command.CommandText = this._datacommand.StroredProcedureName;
                command.Connection.Open();
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to Read Data", exception);
            }

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = (IRepositoryDataModel)new TOut();
                    t.TransformReaderToClass(reader);
                    yield return (TOut)t;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add parameters to command.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        protected void AddParametersToCommand(SqlCommand command)
        {
            foreach (var parameter in this._parameters)
            {
                command.Parameters.AddWithValue(parameter.Value.Name, parameter.Value.TranslatedValue);
            }
        }

        #endregion
    }
}