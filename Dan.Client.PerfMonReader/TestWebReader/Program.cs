// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TestWebReader
{
    using System;
    using System.Collections.Generic;

    using Dan.Common.Messages;

    using WebMetricsReader;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        #region Methods

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            var reader = new EventsReader();
            List<MetricValue> result = reader.GetMetricsByGroupNameAsync("fred").Result;
            foreach (var metricValue in result)
            {
                Console.WriteLine(metricValue.Id);
                Console.WriteLine(metricValue.Raw);
            }

            var result2 = reader.GetGroupsAsync("%").Result;

            foreach (var groupName in result2)
            {
                Console.WriteLine(groupName.Id + groupName.Groupname);
            }

            Console.ReadKey();
        }

        #endregion
    }
}