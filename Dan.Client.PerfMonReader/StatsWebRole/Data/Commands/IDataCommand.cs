// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataCommand.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The DataCommand interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.Commands
{
    /// <summary>
    /// The DataCommand interface.
    /// </summary>
    public interface IDataCommand
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the connection string key.
        /// </summary>
        string ConnectionStringKey { get; set; }

        /// <summary>
        /// Gets or sets the strored procedure name.
        /// </summary>
        string StroredProcedureName { get; set; }

        #endregion
    }
}