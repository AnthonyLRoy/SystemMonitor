using System;

namespace Dan.monitor.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WebMetricsReaderAttribute : Attribute
    {
        public WebMetricsReaderAttribute(bool isActive)
        {
            IsActive = isActive;
        }
        public bool IsActive { get; set; }
    }
}



