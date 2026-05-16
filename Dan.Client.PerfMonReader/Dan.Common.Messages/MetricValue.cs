using System;
using Dan.monitor.Common;

namespace Dan.Common.Messages
{
    public class MetricValue : BaseMetricValue 
    {
        public MetricValue()
        {
            
        }
        public MetricValue(double raw,double calculated,string id,DateTime created)
        {
            TypeCode = 1;
            Raw = raw;
            Calculated = calculated;
            Id = id;
            Created = created;
            TextMessage = string.Empty;
        }

        public MetricValue(double statusCode, string textmessage,string id)
        {
            Id = id;
            TypeCode = 2;
            Raw = statusCode;
            Calculated = statusCode;
            TextMessage = textmessage;
            Created = DateTime.Now;
        }
    }
}
