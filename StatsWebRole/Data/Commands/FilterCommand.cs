// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterCommand.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The filter command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.Commands
{
    /// <summary>
    /// The filter command.
    /// </summary>
    public class FilterCommand : BaseCommand, IDataCommand
    {
        #region Constants

        /// <summary>
        /// The c stored procedure.
        /// </summary>
        private const string CStoredProcedure = "dbo.sp_GetGroups";

        /// <summary>
        /// The cconnectionstring key.
        /// </summary>
        private const string CconnectionstringKey = "dbconnectionstring";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterCommand"/> class.
        /// </summary>
        public FilterCommand()
        {
            this.StroredProcedureName = CStoredProcedure;
            this.ConnectionStringKey = CconnectionstringKey;
        }

        #endregion
    }
}