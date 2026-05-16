// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryEventUsageStatistics.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The repository event usage statistics.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.RepositoryModels
{
    using System;
    using System.Data;

    using Stats.Web.Api.Data.RepoModels;

    /// <summary>
    /// The repository event usage statistics.
    /// </summary>
    public class RepositoryEventUsageStatistics : BaseDataType, IRepositoryDataModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryEventUsageStatistics"/> class.
        /// </summary>
        public RepositoryEventUsageStatistics()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryEventUsageStatistics"/> class.
        /// </summary>
        /// <param name="eventDate">
        /// The event date.
        /// </param>
        /// <param name="hour">
        /// The hour.
        /// </param>
        /// <param name="total">
        /// The total.
        /// </param>
        /// <param name="eventPutAwayEvent">
        /// The event put away event.
        /// </param>
        /// <param name="skuDespatchedEvent">
        /// The sku despatched event.
        /// </param>
        /// <param name="stockAdjustmentEvent">
        /// The stock adjustment event.
        /// </param>
        /// <param name="unableToServeEvent">
        /// The unable to serve event.
        /// </param>
        /// <param name="locationLockedEvent">
        /// The location locked event.
        /// </param>
        /// <param name="locationUnlockedEvent">
        /// The location unlocked event.
        /// </param>
        /// <param name="allocationSuccessEvent">
        /// The allocation success event.
        /// </param>
        /// <param name="allocationFailedEvent">
        /// The allocation failed event.
        /// </param>
        /// <param name="manualPostWmsCancellationEvent">
        /// The manual post wms cancellation event.
        /// </param>
        public RepositoryEventUsageStatistics(
            DateTime eventDate, 
            string hour, 
            int total, 
            int eventPutAwayEvent, 
            int skuDespatchedEvent, 
            int stockAdjustmentEvent, 
            int unableToServeEvent, 
            int locationLockedEvent, 
            int locationUnlockedEvent, 
            int allocationSuccessEvent, 
            int allocationFailedEvent, 
            int manualPostWmsCancellationEvent)
        {
            this.EventDate = eventDate;
            this.Hour = hour;
            this.Total = total;
            this.EventPutAwayEvent = eventPutAwayEvent;
            this.SkuDespatchedEvent = skuDespatchedEvent;
            this.StockAdjustmentEvent = stockAdjustmentEvent;
            this.UnableToServeEvent = unableToServeEvent;
            this.LocationLockedEvent = locationLockedEvent;
            this.LocationUnlockedEvent = locationUnlockedEvent;
            this.AllocationSuccessEvent = allocationSuccessEvent;
            this.AllocationFailedEvent = allocationFailedEvent;
            this.ManualPostWmsCancellationEvent = manualPostWmsCancellationEvent;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the allocation failed event.
        /// </summary>
        public int AllocationFailedEvent { get; set; }

        /// <summary>
        /// Gets or sets the allocation success event.
        /// </summary>
        public int AllocationSuccessEvent { get; set; }

        /// <summary>
        /// Gets or sets the event date.
        /// </summary>
        public DateTime EventDate { get; set; }

        /// <summary>
        /// Gets or sets the event put away event.
        /// </summary>
        public int EventPutAwayEvent { get; set; }

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        public string Hour { get; set; }

        /// <summary>
        /// Gets or sets the location locked event.
        /// </summary>
        public int LocationLockedEvent { get; set; }

        /// <summary>
        /// Gets or sets the location unlocked event.
        /// </summary>
        public int LocationUnlockedEvent { get; set; }

        /// <summary>
        /// Gets or sets the manual post wms cancellation event.
        /// </summary>
        public int ManualPostWmsCancellationEvent { get; set; }

        /// <summary>
        /// Gets or sets the sku despatched event.
        /// </summary>
        public int SkuDespatchedEvent { get; set; }

        /// <summary>
        /// Gets or sets the stock adjustment event.
        /// </summary>
        public int StockAdjustmentEvent { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the unable to serve event.
        /// </summary>
        public int UnableToServeEvent { get; set; }

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
            this.EventDate = (DateTime)reader["eventDate"];
            this.Hour = reader["hour"].ToString();
            this.Total = (int)reader["total"];
            this.EventPutAwayEvent = (int)reader["PutAwayEvent"];
            this.SkuDespatchedEvent = (int)reader["SkuDespatchedEvent"];
            this.StockAdjustmentEvent = (int)reader["stockAdjustmentEvent"];
            this.UnableToServeEvent = (int)reader["unableToServeEvent"];
            this.LocationLockedEvent = (int)reader["locationLockedEvent"];
            this.LocationUnlockedEvent = (int)reader["locationUnlockedEvent"];
            this.AllocationSuccessEvent = (int)reader["allocationSuccessEvent"];
            this.AllocationFailedEvent = (int)reader["allocationFailedEvent"];
            this.ManualPostWmsCancellationEvent = (int)reader["manualPostWmsCancellationEvent"];
        }

        #endregion
    }
}