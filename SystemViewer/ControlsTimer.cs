// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlsTimer.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   Defines the ControlsTimer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;

    using Dan.Common.Client;
    using Dan.monitor.Common;

    public class ControlsTimer
    {

        private readonly Timer controlUpdateScheduler;
        public IEnumerable<IMonitorControl> Controls { get; set; }
        public ControlsTimer()
        {
            this.controlUpdateScheduler = new Timer(
                this.Process,
                null,
                Timeout.InfiniteTimeSpan,
                Timeout.InfiniteTimeSpan);
        }

        public ControlsTimer(IEnumerable<IMonitorControl> controls)
            : this()
        {
            this.Controls = controls;
           
        }

        private void SetGroups()
        {
            Application.Current.Dispatcher.BeginInvoke(
                new Action(
                    () =>
                        {
                            List<GroupName> groupnames;
                            groupnames = (from v in this.Controls
                                          where v.MonitoringEvent != null
                                          select v.MonitoringEvent.Split(":".ToCharArray())[0]
                                          into keys
                                          where !TimedCollector.Tasks.ContainsKey(keys)
                                          select new GroupName("TestReader", keys)).ToList();

                            foreach (GroupName gname in groupnames)
                                TimedCollector.AddTask(
                                    gname.Groupname,
                                    new GroupNameTask(gname, gname.Groupname, new TimeSpan(0, 0, 0,10)));
                        }));
        }





        public bool StartTimer()
        {
            this.controlUpdateScheduler.Change(100, 10000);
            SetGroups();
            return true;
        }

        public bool StopTimer()
        {
            this.controlUpdateScheduler.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            return true;
        }
        private void Process(object state)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(
                () =>
                    {
                        foreach (var monitorControl in this.Controls)
                        {
                            if (monitorControl.MonitoringEvent != null)
                            {
                                var result = monitorControl.MonitoringEvent.Split(":".ToCharArray());
                                var x = WebReaders.GetMetricValueFromCollectedData(result[0], result[1]);

                                if (x != null)
                                {
                                    monitorControl.Value = x;
                                }
                                else
                                {
                                    monitorControl.Value = null;
                                }
                            }
                        }
                    }));

        }
    }
}
