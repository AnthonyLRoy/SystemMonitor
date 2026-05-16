// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceStatistics.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The service statistics.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.Commands
{
    /// <summary>
    /// The service statistics.
    /// </summary>
    public class ServiceStatistics : BaseCommand, IDataCommand
    {
        #region Constants

        /// <summary>
        /// The c stored procedure.
        /// </summary>
        private const string CStoredProcedure = "dbo.sp_GetStatsByGroup";

        /// <summary>
        /// The cconnectionstring key.
        /// </summary>
        private const string CconnectionstringKey = "dbconnectionstring";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceStatistics"/> class.
        /// </summary>
        public ServiceStatistics()
        {
            this.StroredProcedureName = CStoredProcedure;
            this.ConnectionStringKey = CconnectionstringKey;
        }

        #endregion
    }
}