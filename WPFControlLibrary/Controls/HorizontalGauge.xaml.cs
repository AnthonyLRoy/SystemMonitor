// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalGauge.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   The horizontal gauge.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WPFControlLibrary.Controls
{
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    using Dan.monitor.Common;

    using WPFControlLibrary.Base;

    /// <summary>
    /// The horizontal gauge.
    /// </summary>
    [MonitorControl(true)]
    public partial class HorizontalGauge : BaseUserControl, IMonitorControl
    {
        #region Fields

        /// <summary>
        /// The _h progress bar.
        /// </summary>
        private ProgressBar _hProgressBar;

        /// <summary>
        /// The _progress bar value.
        /// </summary>
        private float _progressBarValue;

        /// <summary>
        /// The labels.
        /// </summary>
        private Dictionary<string, Label> labels = new Dictionary<string, Label>();

        /// <summary>
        /// The progress bar forground.
        /// </summary>
        private SolidColorBrush progressBarForground;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalGauge"/> class.
        /// </summary>
        public HorizontalGauge()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.NewValueReceived += this.HorizontalGauge_NewValueReceived;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the progress bar forground.
        /// </summary>
        public SolidColorBrush ProgressBarForground
        {
            get
            {
                return this.progressBarForground;
            }

            set
            {
                if (this.progressBarForground != value)
                {
                    this.progressBarForground = value;
                    this.CallPropertyChange("ProgressBarForground");
                }
            }
        }

        /// <summary>
        /// Gets or sets the progress bar value.
        /// </summary>
        public float ProgressBarValue
        {
            get
            {
                return this._progressBarValue;
            }

            set
            {
                this._progressBarValue = value;
                this.CheckColorStatus(value);
                this.CallPropertyChange("ProgressBarValue");
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The control description.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ControlDescription()
        {
            return "Horizontal Gauge Meter ";
        }

        /// <summary>
        /// The icon.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string icon()
        {
            return "/Images/MeterBar.png";
        }

        #endregion

        #region Methods

        /// <summary>
        /// The check color status.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        private void CheckColorStatus(float value)
        {
            if (value <= this.Ok)
            {
                this.ProgressBarForground = new SolidColorBrush(this.OkColor);
            }

            if (value > this.Warn)
            {
                this.ProgressBarForground = new SolidColorBrush(this.WarnColor);
            }

            if (value >= this.Alert)
            {
                this.ProgressBarForground = new SolidColorBrush(this.AlertColor);
            }
        }

        /// <summary>
        /// The horizontal gauge_ new value received.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        private void HorizontalGauge_NewValueReceived(NewMetricEventValue e)
        {
            if (e.Value != null)
            {
                this.ProgressBarValue = (float)e.Value.Raw;
            }
        }
        #endregion
    }
}