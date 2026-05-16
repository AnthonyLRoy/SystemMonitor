// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventListViewModel.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The event list view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using SystemViewer.Models;

    using Dan.monitor.Common;

    /// <summary>
    /// The event list view model.
    /// </summary>
    public class EventListViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The _model.
        /// </summary>
        private EventListModel _model = new EventListModel();

        /// <summary>
        /// The selected group name.
        /// </summary>
        private GroupName selectedGroupName;

        #endregion

        #region Public Events

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the active groups.
        /// </summary>
        public List<GroupName> ActiveGroups
        {
            get
            {
                return this._model.ActiveGroups();
            }
        }

        /// <summary>
        /// Gets the event list.
        /// </summary>
        public List<string> EventList
        {
            get
            {
                return this._model.GetEventsByGroup(this.SelectedGroup);
            }
        }

        /// <summary>
        /// Gets or sets the selected group.
        /// </summary>
        public GroupName SelectedGroup { get; set; }

        #endregion
    }
}