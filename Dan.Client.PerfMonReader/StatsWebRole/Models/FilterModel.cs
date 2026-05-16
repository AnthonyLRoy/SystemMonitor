// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterModel.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The filter model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Models
{
    /// <summary>
    /// The filter model.
    /// </summary>
    public class FilterModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterModel"/> class.
        /// </summary>
        public FilterModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterModel"/> class.
        /// </summary>
        /// <param name="groupname">
        /// The groupname.
        /// </param>
        public FilterModel(string groupname)
        {
            this.GroupName = groupname;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the group name.
        /// </summary>
        public string GroupName { get; set; }

        #endregion
    }
}