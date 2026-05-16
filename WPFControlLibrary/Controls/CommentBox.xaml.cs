// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentBox.xaml.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   Interaction logic for CommentBox.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WPFControlLibrary.Controls
{
    using System.Drawing;

    using Dan.monitor.Common;
    using Dan.monitor.Common.Dan.Common.Messages;

    using WPFControlLibrary.Base;

    /// <summary>
    /// Interaction logic for CommentBox.xaml
    /// </summary>
    [MonitorControl(true)]
    public partial class CommentBox : BaseUserControl, IMonitorControl
    {
        #region Constructors and Destructors

        private IMetricValue _currentValue;
        public static string ControlDescription()
        {
            return "Danny";
        }
        public static string icon()
        {
            return "Images/MeterBar.ico";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentBox"/> class.
        /// </summary>
        public CommentBox()
        {
            this.InitializeComponent();
        }
        public IMetricValue Value
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