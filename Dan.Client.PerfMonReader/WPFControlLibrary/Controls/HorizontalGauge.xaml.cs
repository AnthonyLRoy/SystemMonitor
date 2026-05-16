using System.Windows.Media;

namespace WPFControlLibrary.Controls
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    using Dan.monitor.Common;

    using WPFControlLibrary.Base;



    [MonitorControl(true)]

    public partial class HorizontalGauge : BaseUserControl, IMonitorControl
    {
        private ProgressBar hProgressBar;
        private Dictionary<string, Label> labels = new Dictionary<string, Label>();
        private float _progressBarValue;
        private SolidColorBrush progressBarForground;
        public HorizontalGauge()
        {

            InitializeComponent();
            RebindControls();
            NewValueReceived+=HorizontalGauge_NewValueReceived;
        }


        private void RebindControls()
        {
            var grid = this.FindChild<Grid>();
            hProgressBar = this.FindChild<ProgressBar>();
            foreach (var child in grid.FindChildren<Label>(x=> !string.IsNullOrEmpty(x.Tag.ToString())))
            {
                labels.Add(child.Tag.ToString(),child);
            }

            var descriptionBinding = new Binding("Description");
            descriptionBinding.Source = this;
            descriptionBinding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(labels["lblDescription"], Label.ContentProperty, descriptionBinding); 
        }

        private void HorizontalGauge_NewValueReceived(NewMetricEventValue e)
        {
            if (e.Value != null)
            {
                ProgressBarValue = (float)e.Value.Raw;
            }
        }

        public static string ControlDescription()
        {
            return "Horizontal Gauge Meter ";
        }

        public static string icon()
        {
            return "Images/MeterBar.ico";
        }

        public float ProgressBarValue
        {
            get
            {
                return this._progressBarValue;
            }
            set
            {
                this._progressBarValue = value;
                CheckColorStatus(value);
                this.CallPropertyChange("ProgressBarValue");
            }
        }

        public SolidColorBrush ProgressBarForground
        {
            get
            {
                return this.progressBarForground;
            }
            set
            {
                if (this.progressBarForground != value) this.progressBarForground = value;
                this.CallPropertyChange("ProgressBarForground");
            }
        }

        private void CheckColorStatus(float value)
        {
                if (value <= Ok)
                {
                    ProgressBarForground = new SolidColorBrush(OkColor);
                }

                if (value > Warn)
                {
                    ProgressBarForground = new SolidColorBrush(WarnColor);
                }
                if (value >= Alert)
                {
                    ProgressBarForground = new SolidColorBrush(AlertColor);
                }
        }
    }
}
