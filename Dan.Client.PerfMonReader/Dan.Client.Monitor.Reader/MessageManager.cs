using Autofac;
using Dan.Common.Messages;
using ServiceBusClient;

namespace Dan.Client.Monitor.Reader
{
    public class MessageManager : IMessageManager
    {
        private readonly IMessageService _serviceBusservice = autofac.Container.Resolve<IMessageService>();
        public void SendMessages(MonitorMessageCollection monitorMessageCollection)
        {
            _serviceBusservice.Publish(monitorMessageCollection);
        }
    }
}