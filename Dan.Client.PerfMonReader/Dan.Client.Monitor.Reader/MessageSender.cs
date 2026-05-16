using System.Collections.Generic;
using Dan.Common.Messages;
using Dan.Common.Messages.Helpers;
using Dan.Common.Messages.Performance;
using Newtonsoft.Json;

namespace Dan.Client.Monitor.Reader
{
    public class MessageSender
    {
        internal void Send(List<PerfCounter> performanceObjects)
        {
            var message = new MonitorMessage(new CustomXmlSerializer());
            message.Body = JsonConvert.SerializeObject(performanceObjects);
            message.Properties.Add("Server", "Hello");
            message.Properties.Add("MachineName", "SSDF");
        }
    }
}