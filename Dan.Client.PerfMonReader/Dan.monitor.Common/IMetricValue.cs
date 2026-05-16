// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMetricValue.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   IMetricValue.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.monitor.Common
{
    namespace Dan.Common.Messages
    {
        using System;

        public interface IMetricValue
        {
            double Raw { get; set; }

            int TypeCode { get; set; }
            string TextMessage { get; set; }
            double Calculated { get; set; }
            string Id { get; set; }
            DateTime Created { get; set; }
        }
    }
}