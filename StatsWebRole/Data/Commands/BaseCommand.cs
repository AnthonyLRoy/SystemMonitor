// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseCommand.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The base command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.Commands
{
    /// <summary>
    /// The base command.
    /// </summary>
    public abstract class BaseCommand
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the connection string key.
        /// </summary>
        public string ConnectionStringKey { get; set; }

        /// <summary>
        /// Gets or sets the strored procedure name.
        /// </summary>
        public string StroredProcedureName { get; set; }

        #endregion
    }
}