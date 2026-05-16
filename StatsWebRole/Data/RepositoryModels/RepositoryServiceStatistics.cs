// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryServiceStatistics.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The repository service statistics.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.RepositoryModels
{
    using System;
    using System.Data;

    using Stats.Web.Api.Data.RepoModels;

    /// <summary>
    /// The repository service statistics.
    /// </summary>
    public class RepositoryServiceStatistics : BaseDataType, IRepositoryDataModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryServiceStatistics"/> class.
        /// </summary>
        public RepositoryServiceStatistics()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryServiceStatistics"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="rawValue">
        /// The raw value.
        /// </param>
        /// <param name="calculatedValue">
        /// The calculated value.
        /// </param>
        /// <param name="eventCreated">
        /// The event created.
        /// </param>
        /// <param name="eventReceived">
        /// The event received.
        /// </param>
        public RepositoryServiceStatistics(
            string id, 
            float rawValue, 
            float calculatedValue, 
            DateTime eventCreated, 
            DateTime eventReceived)
        {
            this.ID = id;
            this.Raw = rawValue;
            this.Calculated = calculatedValue;
            this.Created = eventCreated;
            this.Received = eventReceived;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the calculated.
        /// </summary>
        public float Calculated { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the raw.
        /// </summary>
        public float Raw { get; set; }

        /// <summary>
        /// Gets or sets the received.
        /// </summary>
        public DateTime Received { get; set; }

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
            this.ID = (string)reader["ID"];
            this.Raw = float.Parse(reader["RawValue"].ToString());
            this.Calculated = float.Parse(reader["CalculatedValue"].ToString());
            this.Created = DateTime.Parse(reader["EventCreated"].ToString()).ToUniversalTime();
            this.Received = DateTime.Parse(reader["EventReceived"].ToString()).ToUniversalTime();
        }

        #endregion
    }
}