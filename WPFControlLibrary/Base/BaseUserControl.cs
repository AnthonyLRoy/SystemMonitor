// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseUserControl.cs" company="">
//   
// </copyright>
// <summary>
//   The base user control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WPFControlLibrary.Base
{
    using System;
    using System.ComponentModel;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    using Dan.monitor.Common;
    using Dan.monitor.Common.Dan.Common.Messages;

    /// <summary>
    /// The base user control.
    /// </summary>
    public abstract class BaseUserControl : UserControl, INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The alert.
        /// </summary>
        private float alert;

        /// <summary>
        /// The alert color.
        /// </summary>
        private Color alertColor;

        /// <summary>
        /// The auto max.
        /// </summary>
        private bool autoMax;

        /// <summary>
        /// The auto min.
        /// </summary>
        private bool autoMin;

        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The email on alert.
        /// </summary>
        private bool emailOnAlert;

        /// <summary>
        /// The email receipient.
        /// </summary>
        private EmailMessage emailReceipient;

        /// <summary>
        /// The max value.
        /// </summary>
        private float maxValue;

        /// <summary>
        /// The max value string.
        /// </summary>
        private string maxValueString;

        /// <summary>
        /// The min value.
        /// </summary>
        private float minValue;

        /// <summary>
        /// The min value string.
        /// </summary>
        private string minValueString;

        /// <summary>
        /// The monitoring event.
        /// </summary>
        private string monitoringEvent;

        /// <summary>
        /// The ok.
        /// </summary>
        private float ok;

        /// <summary>
        /// The ok color.
        /// </summary>
        private Color okColor;

        /// <summary>
        /// The value.
        /// </summary>
        private IMetricValue value;

        /// <summary>
        /// The warn.
        /// </summary>
        private float warn;

        /// <summary>
        /// The warn color.
        /// </summary>
        private Color warnColor;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUserControl"/> class.
        /// </summary>
        public BaseUserControl()
        {
            this.MouseDoubleClick += this.UserControl_MouseDoubleClick;
        }

        #endregion

        #region Delegates

        /// <summary>
        /// The new value received event handler.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void NewValueReceivedEventHandler(NewMetricEventValue e);

        #endregion

        #region Public Events

        /// <summary>
        /// The double clicked.
        /// </summary>
        public event EventHandler DoubleClicked;

        /// <summary>
        /// The new value received.
        /// </summary>
        public event NewValueReceivedEventHandler NewValueReceived = delegate { };

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        public float Alert
        {
            get
            {
                return this.alert;
            }

            set
            {
                this.alert = value;
            }
        }

        /// <summary>
        /// Gets or sets the alert color.
        /// </summary>
        public Color AlertColor
        {
            get
            {
                return this.alertColor;
            }

            set
            {
                this.alertColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether auto max.
        /// </summary>
        public virtual bool AutoMax
        {
            get
            {
                return this.autoMax;
            }

            set
            {
                this.autoMax = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether auto min.
        /// </summary>
        public virtual bool AutoMin
        {
            get
            {
                return this.autoMin;
            }

            set
            {
                this.autoMin = value;
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public virtual string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Description"));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether email on alert.
        /// </summary>
        public virtual bool EmailOnAlert
        {
            get
            {
                return this.emailOnAlert;
            }

            set
            {
                this.emailOnAlert = value;
            }
        }

        /// <summary>
        /// Gets or sets the email receipient.
        /// </summary>
        public virtual EmailMessage EmailReceipient
        {
            get
            {
                return this.emailReceipient;
            }

            set
            {
                this.emailReceipient = value;
            }
        }

        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        public virtual float MaxValue
        {
            get
            {
                return this.maxValue;
            }

            set
            {
                this.maxValue = value;
                this.MaxValueString = value.ToString();
                this.PropertyChanged(this, new PropertyChangedEventArgs("maxValue"));
            }
        }

        /// <summary>
        /// Gets or sets the max value string.
        /// </summary>
        public string MaxValueString
        {
            get
            {
                return this.maxValueString;
            }

            set
            {
                this.maxValueString = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("MaxValueString"));
            }
        }

        /// <summary>
        /// Gets or sets the min value.
        /// </summary>
        public virtual float MinValue
        {
            get
            {
                return this.minValue;
            }

            set
            {
                this.minValue = value;
                this.MinValueString = value.ToString();
                this.PropertyChanged(this, new PropertyChangedEventArgs("minValue"));
            }
        }

        /// <summary>
        /// Gets or sets the min value string.
        /// </summary>
        public string MinValueString
        {
            get
            {
                return this.minValueString;
            }

            set
            {
                this.minValueString = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("MinValueString"));
            }
        }

        /// <summary>
        /// Gets or sets the monitoring event.
        /// </summary>
        public string MonitoringEvent
        {
            get
            {
                return this.monitoringEvent;
            }

            set
            {
                this.monitoringEvent = value;
            }
        }

        /// <summary>
        /// Gets or sets the ok.
        /// </summary>
        public virtual float Ok
        {
            get
            {
                return this.ok;
            }

            set
            {
                this.ok = value;
            }
        }

        /// <summary>
        /// Gets or sets the ok color.
        /// </summary>
        public virtual Color OkColor
        {
            get
            {
                return this.okColor;
            }

            set
            {
                this.okColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public virtual IMetricValue Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
                this.NewValueReceived(new NewMetricEventValue(value));
            }
        }

        /// <summary>
        /// Gets or sets the warn.
        /// </summary>
        public virtual float Warn
        {
            get
            {
                return this.warn;
            }

            set
            {
                this.warn = value;
            }
        }

        /// <summary>
        /// Gets or sets the warn color.
        /// </summary>
        public virtual Color WarnColor
        {
            get
            {
                return this.warnColor;
            }

            set
            {
                this.warnColor = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The call property change.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        internal void CallPropertyChange(string propertyName)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// The user control_ mouse double click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.DoubleClicked(sender, e);
        }

        #endregion
    }

    /// <summary>
    /// The new metric event value.
    /// </summary>
    public class NewMetricEventValue
    {
        #region Fields

        /// <summary>
        /// The value.
        /// </summary>
        public IMetricValue Value;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewMetricEventValue"/> class.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public NewMetricEventValue(IMetricValue e)
        {
            this.Value = e;
        }

        #endregion
    }
}