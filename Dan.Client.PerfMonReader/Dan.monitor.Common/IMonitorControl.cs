// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMonitorControl.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The MonitorControl interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.monitor.Common
{
    using System.Collections.Generic;
    using System.Windows.Media;

    using global::Dan.monitor.Common.Dan.Common.Messages;

    /// <summary>
    /// The MonitorControl interface.
    /// </summary>
    public interface IMonitorControl
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        float Alert { get; set; }

        /// <summary>
        /// Gets or sets the alert color.
        /// </summary>
        Color AlertColor { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether email on alert.
        /// </summary>
        bool EmailOnAlert { get; set; }

        /// <summary>
        /// Gets or sets the email receipient.
        /// </summary>
        EmailMessage EmailReceipient { get; set; }

        /// <summary>
        /// Gets or sets the monitoring event.
        /// </summary>
        string MonitoringEvent { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the ok.
        /// </summary>
        float Ok { get; set; }

        /// <summary>
        /// Gets or sets the ok color.
        /// </summary>
        Color OkColor { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        IMetricValue Value { get; set; }

        /// <summary>
        /// Gets or sets the warn.
        /// </summary>
        float Warn { get; set; }

        /// <summary>
        /// Gets or sets the warn color.
        /// </summary>
        Color WarnColor { get; set; }

        float MaxValue { get; set; }
        bool AutoMax { get; set; }

        float MinValue { get; set; }
        bool AutoMin { get; set; }

        #endregion
    }
}