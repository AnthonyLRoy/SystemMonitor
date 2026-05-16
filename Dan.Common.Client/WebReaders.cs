// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebReaders.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The web readers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Client
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Dan.Common.Messages;
    using Dan.monitor.Common;
    using Dan.monitor.Common.Dan.Common.Messages;

    /// <summary>
    /// The web readers.
    /// </summary>
    public static class WebReaders
    {
        #region Static Fields

        /// <summary>
        /// The _groups.
        /// </summary>
        private static Task<List<GroupName>> _groups;

        /// <summary>
        /// The _readers.
        /// </summary>
        private static Dictionary<string, IWebMetricsReader<MetricValue>> _readers;

        /// <summary>
        /// The _values.
        /// </summary>
        private static ConcurrentDictionary<string, List<MetricValue>> _values = new ConcurrentDictionary<string, List<MetricValue>>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the active readers.
        /// </summary>
        public static Dictionary<string, IWebMetricsReader<MetricValue>> ActiveReaders
        {
            get
            {
                return _readers;
            }
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        public static List<GroupName> Groups
        {
            get
            {
                return _groups.Result;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The collect values.
        /// </summary>
        /// <param name="groups">
        /// The groups.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task CollectValues(IEnumerable<GroupName> groups)
        {
            foreach (var group in groups)
            {
                var result = GetEventsByGroup(group).Result;
                if (result != null)
                {
                    _values.AddOrUpdate(@group.Groupname, result, (key, existingValue) => result);
                }
            }
        }

        /// <summary>
        /// The get metric value by name.
        /// </summary>
        /// <param name="group">
        /// The group.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="IMetricValue"/>.
        /// </returns>
        public static IMetricValue GetMetricValueByName(GroupName group, MetricValue value)
        {
            return _values.First(x => x.Key == group.Groupname).Value.First(y => y.Id == value.Id);
        }

        /// <summary>
        /// The get metric value from collected data.
        /// </summary>
        /// <param name="groupname">
        /// The groupname.
        /// </param>
        /// <param name="metricid">
        /// The metricid.
        /// </param>
        /// <returns>
        /// The <see cref="MetricValue"/>.
        /// </returns>
        public static MetricValue GetMetricValueFromCollectedData(string groupname, string metricid)
        {
            if (_values.ContainsKey(groupname))
            {
                return _values.First(x => x.Key == groupname).Value.First(x => x.Id == metricid);
            }
            return null;
        }

        /// <summary>
        /// The init.
        /// </summary>
        public static void Init()
        {
            _readers = new Dictionary<string, IWebMetricsReader<MetricValue>>();
            foreach (var instanceReader in Readers().Select(reader => Activator.CreateInstance((Type)reader) as IWebMetricsReader<MetricValue>))
            {
                _readers.Add(instanceReader.Name, instanceReader);
            }

            _groups = GetAllGroups();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get events by group.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<List<MetricValue>> GetEventsByGroup(GroupName groupName)
        {
            var metricValues = await ActiveReaders.First(x => x.Key == groupName.Id).Value.GetMetricsByGroupNameAsync(groupName.Groupname);
            return metricValues;
        }

        /// <summary>
        /// The get all groups.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private static async Task<List<GroupName>> GetAllGroups()
        {
            var results = new List<GroupName>();
            var tasks =
                ActiveReaders.Select(webMetricsReader => webMetricsReader.Value.GetGroupsAsync("%"))
                    .Cast<Task>()
                    .ToList();
            while (tasks.Count > 0)
            {
                var taskResult = (Task<List<GroupName>>)await Task.WhenAny(tasks.ToArray());
                tasks.Remove(taskResult);
                results.AddRange(taskResult.Result);
            }

            return results;
        }

        /// <summary>
        /// The readers.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<object> Readers()
        {
            var types =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(
                        x =>
                        x.GetTypes()
                            .Where(
                                t =>
                                t.GetCustomAttribute<WebEventsReaderAttribue>() != null
                                && t.GetCustomAttribute<WebEventsReaderAttribue>().IsActive));

            return types;
        }

        #endregion
    }
}