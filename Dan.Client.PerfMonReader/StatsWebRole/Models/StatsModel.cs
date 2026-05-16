// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatsModel.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The stats model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Models
{
    using System;

    /// <summary>
    /// The stats model.
    /// </summary>
    public class StatsModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StatsModel"/> class.
        /// </summary>
        public StatsModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatsModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="rawvalue">
        /// The rawvalue.
        /// </param>
        /// <param name="calculatedvalue">
        /// The calculatedvalue.
        /// </param>
        /// <param name="eventcreated">
        /// The eventcreated.
        /// </param>
        /// <param name="eventreceived">
        /// The eventreceived.
        /// </param>
        public StatsModel(
            string id, 
            float rawvalue, 
            float calculatedvalue, 
            DateTime eventcreated, 
            DateTime eventreceived)
        {
            this.ID = id;
            this.Raw = rawvalue;
            this.Calculated = calculatedvalue;
            this.Created = eventcreated;
            this.Received = eventreceived;
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
    }
}