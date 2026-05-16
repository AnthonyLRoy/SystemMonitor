// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupNameTask.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The group name task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SystemViewer
{
    using System;
    using System.Threading;

    using Dan.monitor.Common;

    /// <summary>
    /// The group name task.
    /// </summary>
    public class GroupNameTask
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupNameTask"/> class.
        /// </summary>
        /// <param name="group">
        /// The group.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="delay">
        /// The delay.
        /// </param>
        public GroupNameTask(GroupName group, string id, TimeSpan delay)
        {
            this.Group = group;
            this.Id = id;
            this.Active = false;
            this.Delay = delay;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the delay.
        /// </summary>
        public TimeSpan Delay { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        public GroupName Group { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the timer.
        /// </summary>
        public Timer timer { get; set; }

        #endregion
    }
}