// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebRole.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The web role.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole
{
    using Microsoft.WindowsAzure.ServiceRuntime;

    /// <summary>
    /// The web role.
    /// </summary>
    public class WebRole : RoleEntryPoint
    {
        #region Public Methods and Operators

        /// <summary>
        /// The on start.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            return base.OnStart();
        }

        #endregion
    }
}