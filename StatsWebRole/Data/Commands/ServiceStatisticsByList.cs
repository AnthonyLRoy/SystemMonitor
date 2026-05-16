// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceStatisticsByList.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The service statistics by list.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.Commands
{
    /// <summary>
    /// The service statistics by list.
    /// </summary>
    public class ServiceStatisticsByList : BaseCommand, IDataCommand
    {
        #region Constants

        /// <summary>
        /// The c stored procedure.
        /// </summary>
        private const string CStoredProcedure = "dbo.sp_GetStatsByList";

        /// <summary>
        /// The cconnectionstring key.
        /// </summary>
        private const string CconnectionstringKey = "dbconnectionstring";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceStatisticsByList"/> class.
        /// </summary>
        public ServiceStatisticsByList()
        {
            this.StroredProcedureName = CStoredProcedure;
            this.ConnectionStringKey = CconnectionstringKey;
        }

        #endregion
    }
}