using System;
using Dan.monitor.Common.Dan.Common.Messages;

namespace Dan.Common.Messages
{
    public class BaseMetricValue : IMetricValue
    {
        public int TypeCode { get; set; }
        public string Id { get; set; }

        public double Raw { get; set; }

        public double Calculated { get; set; }

        public DateTime Created { get; set; }

        public string TextMessage { get; set; }

    }
}

