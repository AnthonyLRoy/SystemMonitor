// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupName.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The group name.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.monitor.Common
{
    /// <summary>
    /// The group name.
    /// </summary>
    public class GroupName
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupName"/> class.
        /// </summary>
        /// <param name="readerid">
        /// The readerid.
        /// </param>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        public GroupName(string readerid, string groupName)
        {
            this.Id = readerid;
            this.Groupname = groupName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the groupname.
        /// </summary>
        public string Groupname { get; private set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        #endregion
    }
}