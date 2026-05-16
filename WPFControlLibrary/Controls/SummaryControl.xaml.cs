// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SummaryControl.xaml.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   Interaction logic for DataTableControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WPFControlLibrary.Controls
{
    using System.Drawing;

    using Dan.monitor.Common;
    using Dan.monitor.Common.Dan.Common.Messages;

    using WPFControlLibrary.Base;

    /// <summary>
    /// Interaction logic for DataTableControl.xaml
    /// </summary>
    [MonitorControl(true)]
    public partial class SummaryControl : IMonitorControl
    {

        private IMetricValue _currentValue;
        public static string ControlDescription()
        {
                return "Danny";
        }
        public static string icon()
        {
            return "/Images/MeterBar.ico";
        }

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryControl"/> class.
        /// </summary>
        public SummaryControl()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the update frequency.
        /// </summary>
        public int UpdateFrequency { get; set; }

        public override IMetricValue Value
        {
            get
            {
                return _currentValue;
            }
            set
            {
                _currentValue = value;
                
            }
        }
        #endregion
    }
}