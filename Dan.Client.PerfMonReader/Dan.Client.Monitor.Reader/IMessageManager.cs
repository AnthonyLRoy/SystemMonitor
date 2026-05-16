using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dan.Client.Monitor.Reader
{
    public interface IMessageManager
    {
        void SendMessages(Common.Messages.MonitorMessageCollection monitorMessageCollection);
    }
}
