// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimedCollector.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The timed collector.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SystemViewer
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Dan.Common.Client;
    using Dan.monitor.Common;

    /// <summary>
    /// The timed collector.
    /// </summary>
    public static class TimedCollector
    {
        #region Static Fields

        /// <summary>
        /// The _tasks.
        /// </summary>
        private static Dictionary<string, GroupNameTask> _tasks = new Dictionary<string, GroupNameTask>();

        /// <summary>
        /// The timers.
        /// </summary>
        private static List<Timer> timers = new List<Timer>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        public static Dictionary<string, GroupNameTask> Tasks
        {
            get
            {
                return _tasks;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add task.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="newtask">
        /// The newtask.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool AddTask(string name, GroupNameTask newtask)
        {
            if (!_tasks.ContainsKey(name))
            {
                _tasks.Add(name, newtask);
                StartTask(name);
            }

            return true;
        }

        /// <summary>
        /// The remove task.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool RemoveTask(string name)
        {
            _tasks.Remove(name);
            return true;
        }

        /// <summary>
        /// The start task.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool StartTask(string name)
        {
            var taskdata = _tasks[name];
            taskdata.Active = true;
            var timer = new Timer(Process, taskdata, taskdata.Delay, Timeout.InfiniteTimeSpan);
            taskdata.timer = timer;
            timers.Add(timer);
            return true;
        }

        /// <summary>
        /// The stop all tasks.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool StopAllTasks()
        {
            return true;
        }

        /// <summary>
        /// The stop tasks.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool StopTasks(string name)
        {
            _tasks[name].Active = false;
            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The process.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        private static async void Process(object state)
        {
            var taskData = (GroupNameTask)state;
            await WebReaders.CollectValues(new List<GroupName> { taskData.Group });
            taskData.timer.Change(taskData.Delay, Timeout.InfiniteTimeSpan);
        }

        #endregion
    }
}