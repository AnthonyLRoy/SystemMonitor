// --------------------------------------------------------------------------------------------------------------------
// <copyright file="connections.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The connection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data
{
    /// <summary>
    /// The connection.
    /// </summary>
    public class Connection
    {
        #region Fields

        /// <summary>
        /// The _connectionstring.
        /// </summary>
        private readonly string _connectionstring;

        /// <summary>
        /// The _key.
        /// </summary>
        private readonly string _key;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="connectionstring">
        /// The connectionstring.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        public Connection(string connectionstring, string key)
        {
            this._connectionstring = connectionstring;
            this._key = key;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return this._connectionstring;
            }
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key
        {
            get
            {
                return this._key;
            }
        }

        #endregion
    }
}