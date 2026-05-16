using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Dan.Client.Monitor.Reader;
using Dan.Common.Messages;


using Quartz;
using Autofac;


namespace PerMon
{

    //public class PerfCounterReader : IJob
    //{
    //    IMonitorCommunicationManager _manager;
    //    System.Diagnostics.PerformanceCounter myCounter = new System.Diagnostics.PerformanceCounter();
    //    public PerfCounterReader()
    //    {
    //    }

    //    public void Execute(IJobExecutionContext context)
    //    {
    //        JobDataMap dataMap = context.JobDetail.JobDataMap;

    //        myCounter.CategoryName = dataMap.GetString("category");
    //        myCounter.CounterName = dataMap.GetString("counter");
    //        myCounter.InstanceName = dataMap.GetString("instance");

    //        float raw = myCounter.NextValue();
    //        System.Threading.Thread.Sleep(1000);
    //        raw = myCounter.NextValue();

    //        _manager = autofac.Container.Resolve<IMonitorCommunicationManager>();
    //        IMessage message = _manager.CreateMonitorMessage(dataMap.GetString("name"), dataMap.GetString("instancename"));
    //        message.Add("Records", raw.ToString());
    //        Console.WriteLine("write to bus raw value = " + raw.ToString());
    //        _manager.TranslateAndSend(message);

    //    }
    //}
}
