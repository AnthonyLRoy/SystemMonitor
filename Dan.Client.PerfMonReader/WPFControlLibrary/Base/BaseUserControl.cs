// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseUserControl.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The base user control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WPFControlLibrary.Base
{
    using System;
    using System.Collections.Generic;
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
        private float alert;

        private Color alertColor;

        private string description;

        private bool emailOnAlert;

        private EmailMessage emailReceipient;

        private IMetricValue value;

        private string monitoringEvent;

        private float ok;

        private Color okColor;

        private float warn;

        private Color warnColor;

        private bool autoMin;

        private bool autoMax;

        private float maxValue;

        private float minValue;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUserControl"/> class.
        /// </summary>
        public BaseUserControl()
        {
            this.MouseDoubleClick += this.UserControl_MouseDoubleClick;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The double clicked.
        /// </summary>
        public event EventHandler DoubleClicked;

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
        /// Gets or sets the description.
        /// </summary>
        public  string Description
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
                    PropertyChanged(this, new PropertyChangedEventArgs("Description"));
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

        public virtual IMetricValue Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
                NewValueReceived(new NewMetricEventValue(value));

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

        public virtual float MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                this.maxValue = value;
                MaxValueString = value.ToString();
                PropertyChanged(this, new PropertyChangedEventArgs("maxValue"));
            }
        }

        public virtual float MinValue
        {
            get
            {
                return this.minValue;
            }
            set
            {
                this.minValue = value;
                MinValueString = value.ToString();
                PropertyChanged(this, new PropertyChangedEventArgs("minValue"));
            }
        }

        public string MinValueString
        {
            get
            {
                return this.minValueString;
            }
            set
            {
                this.minValueString = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MinValueString"));
            }
        }


        public string MaxValueString
        {
            get
            {
                return this.maxValueString;
            }
            set
            {
                this.maxValueString = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MaxValueString"));
            }
        }

        #endregion

        #region Methods

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

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public delegate void NewValueReceivedEventHandler(NewMetricEventValue e);
        public event NewValueReceivedEventHandler NewValueReceived = delegate { }; 

        private string minValueString;
        private string maxValueString;


        void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        internal void CallPropertyChange(string propertyName)
        {
            PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
        }
    }

    public class NewMetricEventValue
    {
        public IMetricValue Value;
        public NewMetricEventValue(IMetricValue e)
        {
            Value = e;

        }
    }


}
