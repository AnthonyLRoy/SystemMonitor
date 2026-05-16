// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GaugeControl.xaml.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The gauge control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WPFControlLibrary.Controls
{
    using System.Drawing;
    using System.Drawing.Text;

    using Dan.monitor.Common;
    using Dan.monitor.Common.Dan.Common.Messages;

    using WPFControlLibrary.Base;

    /// <summary>
    /// The gauge control.
    /// </summary>
    [MonitorControl(true)]
    public partial class GaugeControl : BaseUserControl, IMonitorControl
    {
        private IMetricValue _currentValue;

        #region Constructors and Destructors




        public static string ControlDescription()
        {
            return "Danny";
        }

        public static string icon()
        {
            return "Images/MeterBar.ico";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GaugeControl"/> class.
        /// </summary>
        public GaugeControl()
        {
            this.InitializeComponent();
  
        }

        public override IMetricValue Value
        {
            get
            {
                return this._currentValue;
            }
            set
            {
                this._currentValue = value;
                
            }
        }
        #endregion
    }
}