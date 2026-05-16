// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataTableControl.xaml.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   Interaction logic for DataTableControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WPFControlLibrary.Controls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using Dan.monitor.Common;
    using Dan.monitor.Common.Dan.Common.Messages;

    using WPFControlLibrary.Base;

    /// <summary>
    /// Interaction logic for DataTableControl.xaml
    /// </summary>
    [MonitorControl(true)]
    public partial class DataTableControl : BaseUserControl, IMonitorControl
    {
        private Label label;

        private IMetricValue _currentValue;
        private ProgressBar progressBar;
        public static string ControlDescription()
        {
            return "Danny";
        }

        public static string icon()
        {
            return "Images/MeterBar.ico";
        }

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableControl"/> class.
        /// </summary>
        public DataTableControl()
        {
            this.InitializeComponent();
            var x = DependencyObjExtensions.FindChild<Grid>(this);
            progressBar = DependencyObjExtensions.FindChild<ProgressBar>(x);
            
            //label = DataTableContent.Children.OfType<Label>().FirstOrDefault();
            //progressBar = DataTableContent.Children.OfType<ProgressBar>().FirstOrDefault();
        }

        #endregion

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public override IMetricValue Value
        {
            get
            {
                return _currentValue;
            }

            set
            {

                if (value != null)
                {
                    progressBar.Background = new SolidColorBrush(Colors.GhostWhite);
                    if (value.Raw <= Ok)
                    {
                        progressBar.Foreground = new SolidColorBrush(OkColor);
                    }

                    if (value.Raw > Warn)
                    {
                        progressBar.Foreground = new SolidColorBrush(WarnColor);
                    }

                    if (value.Raw >= Alert)
                    {
                        progressBar.Foreground = new SolidColorBrush(AlertColor);
                    }

                    progressBar.Value = (double)value.Raw;
                }
                else
                {
                    progressBar.Background = new SolidColorBrush(Colors.DarkSlateGray);
                }
            }
        }

        //public override string Description
        //{
        //    get
        //    {
        //        return label.Content.ToString();
        //    }
        //    set
        //    {
        //        label.Content = value;

        //    }
        //}
    }
}