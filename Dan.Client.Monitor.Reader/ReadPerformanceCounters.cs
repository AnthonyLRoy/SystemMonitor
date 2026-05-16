using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dan.Client.Monitor.Reader
{
    class ReadPerformanceCounters
    {
        private List<Common.Messages.Performance.PerfCounter> PerformanceObjects;

        public ReadPerformanceCounters(List<Common.Messages.Performance.PerfCounter> PerformanceObjects)
        {
            // TODO: Complete member initialization
            this.PerformanceObjects = PerformanceObjects;
        }
        internal object Execute()
        {
            throw new NotImplementedException();
        }
    }
}
