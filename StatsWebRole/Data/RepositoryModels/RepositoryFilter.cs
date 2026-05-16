// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryFilter.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The repository filter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.RepositoryModels
{
    using System.Data;

    using Stats.Web.Api.Data.RepoModels;

    /// <summary>
    /// The repository filter.
    /// </summary>
    public class RepositoryFilter : BaseDataType, IRepositoryDataModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFilter"/> class.
        /// </summary>
        public RepositoryFilter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFilter"/> class.
        /// </summary>
        /// <param name="groupname">
        /// The groupname.
        /// </param>
        public RepositoryFilter(string groupname)
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

        #region Public Methods and Operators

        /// <summary>
        /// The transform reader to class.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        public void TransformReaderToClass(IDataReader reader)
        {
            this.GroupName = (string)reader["GroupName"];
        }

        #endregion
    }
}