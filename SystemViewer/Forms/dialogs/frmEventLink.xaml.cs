// --------------------------------------------------------------------------------------------------------------------
// <copyright file="frmEventLink.xaml.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   Interaction logic for frmEventLink.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Forms.dialogs
{
    using System.Windows;
    using System.Windows.Controls;

    using SystemViewer.ViewModel;

    using Dan.monitor.Common;

    /// <summary>
    /// Interaction logic for frmEventLink.xaml
    /// </summary>
    public partial class frmEventLink : Window
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="frmEventLink"/> class.
        /// </summary>
        public frmEventLink()
        {
            this.InitializeComponent();
            this.Eventslist = new EventListViewModel();
            this.DataContext = this.Eventslist;
            this.CboGroups.ItemsSource = this.Eventslist.ActiveGroups;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the eventslist.
        /// </summary>
        private EventListViewModel Eventslist { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The cancel button_ on click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// The cbo groups_ selection changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CboGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Eventslist.SelectedGroup = (GroupName)this.CboGroups.SelectedItem;
            this.LstEventList.ItemsSource = this.Eventslist.EventList;
        }

        /// <summary>
        /// The lst event list_ selection changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void LstEventList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Eventslist.SelectedGroup = (GroupName)this.CboGroups.SelectedItem;
        }

        /// <summary>
        /// The ok button_ on click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        #endregion
    }
}