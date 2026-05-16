// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventListModel.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The event list model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dan.Common.Client;
    using Dan.monitor.Common;

    /// <summary>
    /// The event list model.
    /// </summary>
    public class EventListModel
    {
        #region Public Methods and Operators

        /// <summary>
        /// The active groups.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<GroupName> ActiveGroups()
        {
            return WebReaders.Groups;
        }

        /// <summary>
        /// The get events by group.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<string> GetEventsByGroup(GroupName groupName)
        {
            var x = Task.Run(() => WebReaders.GetEventsByGroup(groupName));
            List<string> eventList = x.Result.Select(y => y.Id).ToList();
            return eventList;
        }

        #endregion
    }
}