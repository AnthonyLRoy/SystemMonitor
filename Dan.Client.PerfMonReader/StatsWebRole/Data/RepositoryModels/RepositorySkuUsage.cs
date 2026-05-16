// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositorySkuUsage.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The repository sku usage.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Stats.Web.Api.DataAccess.RepoModels
{
    using System.Data;

    using Stats.Web.Api.Data.RepoModels;

    using StatsWebRole.Data.RepositoryModels;

    /// <summary>
    /// The repository sku usage.
    /// </summary>
    public class RepositorySkuUsage : BaseDataType, IRepositoryDataModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositorySkuUsage"/> class.
        /// </summary>
        public RepositorySkuUsage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositorySkuUsage"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        public RepositorySkuUsage(string name, int count)
        {
            this.Name = name;
            this.Count = count;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

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
            this.Name = reader["name"].ToString();
            this.Count = (int)reader["Count"];
        }

        #endregion
    }
}