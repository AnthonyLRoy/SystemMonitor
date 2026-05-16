using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dan.Common.Messages;
using Dan.monitor.Common;

namespace SystemViewer.Managers
{
    public class ReaderManager : IReaderManager
    {

        List<IMetricsCollator> _readers = new List<IMetricsCollator>();

        public ReaderManager()
        {
            LoadReaders();
        }

        private void LoadReaders()
        {
            _readers = (List<IMetricsCollator>) AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany
                        (x =>
                            x.GetTypes()
                                .Where(
                                    t =>
                                        t.GetCustomAttribute<EventCollator>() != null &&
                                        t.GetCustomAttribute<EventCollator>().IsActive &&
                                        typeof(IMetricsCollator).IsAssignableFrom(t)));


        }

    }
}
