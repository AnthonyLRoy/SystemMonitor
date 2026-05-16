using System;

namespace Dan.monitor.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventCollator : Attribute
    {
        public EventCollator(bool isActive)
        {
            IsActive = isActive;
        }
        public bool IsActive { get; set; }
    };
}
