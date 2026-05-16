// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagramControlProperties.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The diagram control properties.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SystemViewer.gridClasses
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Media;

    using SystemViewer.gridClasses.Custom;

    using Dan.monitor.Common;
    using Dan.monitor.Common.Dan.Common.Messages;

    /// <summary>
    /// The diagram control properties.
    /// </summary>
    public class DiagramControlProperties : IMonitorControl
    {
        #region Fields

        /// <summary>
        /// The control.
        /// </summary>
        private readonly IMonitorControl control;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagramControlProperties"/> class.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        public DiagramControlProperties(IMonitorControl control)
        {
            // TODO: Complete member initialization
            this.control = control;
            this.AlertColor = control.AlertColor;
            this.Alert = control.Alert;
            this.Warn = control.Warn;
            this.WarnColor = control.WarnColor;
            this.Description = control.Description;
            this.Ok = control.Ok;
            this.OkColor = control.OkColor;
            this.EmailOnAlert = control.EmailOnAlert;
            this.EmailReceipient = control.EmailReceipient;
            this.MonitoringEvent = control.MonitoringEvent;
            this.MaxValue = control.MaxValue;
            this.AutoMax = control.AutoMax;
            this.MinValue = control.MinValue;
            this.AutoMin = control.AutoMin;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        [Category("Colour")]
        [DisplayName(@"Alert")]
        [Description("The threshold limit for alrert to become active")]
        public float Alert
        {
            get
            {
                {
                    return this.control.Alert;
                }
            }

            set
            {
                this.control.Alert = value;
            }
        }

        /// <summary>
        /// Gets or sets the alert color.
        /// </summary>
        [Category("Colour")]
        [DisplayName(@"Alert color")]
        [Description("The Color for the component being in an alert state")]
        [Editor(typeof(System.Drawing.Color), typeof(System.Drawing.Color))]
        public Color AlertColor
        {
            get
            {
                return this.control.AlertColor;
            }

            set
            {
                this.control.AlertColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether auto max.
        /// </summary>
        [Category("Meter Range")]
        [DisplayName(@"Auto Maximum")]
        [Editor(typeof(bool), typeof(bool))]
        [Description("Automatically adjust the maximum value")]
        public bool AutoMax
        {
            get
            {
                return this.control.AutoMax;
            }

            set
            {
                this.control.AutoMax = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether auto min.
        /// </summary>
        [Category("Meter Range")]
        [DisplayName(@"Auto Minimum")]
        [Editor(typeof(bool), typeof(bool))]
        [Description("Sets the minimum Value")]
        public bool AutoMin
        {
            get
            {
                return this.control.AutoMin;
            }

            set
            {
                this.control.AutoMin = value;
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Category("Monitor details")]
        [DisplayName(@"Description Text")]
        [Description("Description that describes monitor control ")]
        public string Description
        {
            get
            {
                return this.control.Description;
            }

            set
            {
                if  (this.control.Description != value) this.control.Description = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether email on alert.
        /// </summary>
        [Editor(typeof(System.Boolean), typeof(System.Boolean))]
        [Category("Email")]
        [DisplayName(@"Email on Alert")]
        [Description("Allow Emails to be sent on Alert")]
        public bool EmailOnAlert
        {
            get
            {
                return this.control.EmailOnAlert;
            }

            set
            {
                this.control.EmailOnAlert = value;
            }
        }

        /// <summary>
        /// Gets or sets the email receipient.
        /// </summary>
                 [Editor(typeof(EmailAttribute), typeof(EmailAttribute))]
        [Category("Email")]
        [DisplayName(@"Email")]
        [Description("Destination email address for alert")]
        public EmailMessage EmailReceipient
        {
            get
            {
                return this.control.EmailReceipient;
            }

            set
            {
                this.control.EmailReceipient = value;
            }
        }

        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        [Category("Meter Range")]
        [DisplayName(@"Maximum Value")]
        [Description("Sets the maximum Value")]
        public float MaxValue
        {
            get
            {
                return this.control.MaxValue;
            }

            set
            {
                this.control.MaxValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the min value.
        /// </summary>
        [Category("Meter Range")]
        [DisplayName(@"Maximum Value")]
        [Description("Sets the maximum Value")]
        public float MinValue
        {
            get
            {
                return this.control.MinValue;
            }

            set
            {
                this.control.MinValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the monitoring event.
        /// </summary>
        [Editor(typeof(MonitorAttribute), typeof(MonitorAttribute))]
        [Category("Monitor details")]
        [DisplayName(@"The Event")]
        [Description("The current Read value Event")]
        public string MonitoringEvent
        {
            get
            {
                return this.control.MonitoringEvent;
            }

            set
            {
                this.control.MonitoringEvent = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Category("Monitor details")]
        [DisplayName(@"Name of the control")]
        [Description("The Control name that has been defined for this form")]
        public string Name
        {
            get
            {
                return this.control.Name;
            }

            set
            {
                this.control.Name = value;
            }
        }

        /// <summary>
        /// Gets or sets the ok.
        /// </summary>
        [Category("Colour")]
        [DisplayName(@"OK")]
        [Description("The threshold limit below which within range is ok")]
        public float Ok
        {
            get
            {
                return this.control.Ok;
            }

            set
            {
                this.control.Ok = value;
            }
        }

        /// <summary>
        /// Gets or sets the ok color.
        /// </summary>
        [Category("Colour")]
        [DisplayName(@"OK color")]
        [Description("The Color for the component being within range")]
        [Editor(typeof(System.Drawing.Color), typeof(System.Drawing.Color))]
        public Color OkColor
        {
            get
            {
                return this.control.OkColor;
            }

            set
            {
                this.control.OkColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Category("Monitor details")]
        [DisplayName(@"The Current Value")]
        [Description("The current Read value ")]
        public IMetricValue Value
        {
            get
            {
                return this.control.Value;
            }

            set
            {
                this.control.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the warn.
        /// </summary>
        [Category("Colour")]
        [DisplayName(@"Warn")]
        [Description("The threshold limit for warning to become active")]
        public float Warn
        {
            get
            {
                return this.control.Warn;
            }

            set
            {
                this.control.Warn = value;
            }
        }

        /// <summary>
        /// Gets or sets the warn color.
        /// </summary>
        [Category("Colour")]
        [DisplayName(@"Warning color")]
        [Editor(typeof(System.Drawing.Color), typeof(System.Drawing.Color))]
        [Description("TThe Color change for a warning")]
        public Color WarnColor
        {
            get
            {
                return this.control.WarnColor;
            }

            set
            {
                this.control.WarnColor = value;
            }
        }

        #endregion
    }
}