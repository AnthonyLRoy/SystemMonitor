// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryFailureRate.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The repository failure rate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Stats.Web.Api.DataAccess.RepoModels
{
    using System;
    using System.Data;

    using Stats.Web.Api.Data.RepoModels;

    using StatsWebRole.Data.RepositoryModels;

    /// <summary>
    /// The repository failure rate.
    /// </summary>
    public class RepositoryFailureRate : BaseDataType, IRepositoryDataModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFailureRate"/> class.
        /// </summary>
        public RepositoryFailureRate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFailureRate"/> class.
        /// </summary>
        /// <param name="orderdate">
        /// The orderdate.
        /// </param>
        /// <param name="success">
        /// The success.
        /// </param>
        /// <param name="failure">
        /// The failure.
        /// </param>
        public RepositoryFailureRate(DateTime orderdate, int success, int failure)
        {
            this.Orderdate = orderdate;
            this.Success = success;
            this.Failure = failure;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the failure.
        /// </summary>
        public int Failure { get; set; }

        /// <summary>
        /// Gets or sets the orderdate.
        /// </summary>
        public DateTime Orderdate { get; set; }

        /// <summary>
        /// Gets or sets the success.
        /// </summary>
        public int Success { get; set; }

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
            this.Orderdate = DateTime.Parse(reader["OrderDate"].ToString());
            this.Success = (int)reader["Success"];
            this.Failure = (int)reader["Fail"];
        }

        #endregion
    }
}