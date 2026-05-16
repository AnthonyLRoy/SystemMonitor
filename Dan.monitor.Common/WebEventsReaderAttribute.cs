using System;


    namespace Dan.monitor.Common
    {
        [AttributeUsage(AttributeTargets.Class)]
        public class WebEventsReaderAttribue: Attribute
        {
            public WebEventsReaderAttribue(bool isActive)
            {
                IsActive = isActive;
            }
            public bool IsActive { get; set; }
        };
    }


