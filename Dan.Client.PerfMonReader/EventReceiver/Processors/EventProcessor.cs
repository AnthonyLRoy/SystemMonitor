using System.Runtime.Remoting.Messaging;
using Dan.Common.Messages;
using EventReceiver.Data;
using Microsoft.ServiceBus.Messaging;

namespace EventReceiver.Processors
{
    using System;

    public interface IEventProcessor
        {
            /// <summary>
            /// Remove the Properties from the messages
            /// </summary>
            /// <param name="message">the message received </param>
            void Process(BrokeredMessage message);
        }

        public class EventProcessor : IEventProcessor
        {
            /// <summary>
            /// Remove the Properties from the messages
            /// </summary>
            /// <param name="brokeredmessage">The New brokered Message</param>
            public void Process(BrokeredMessage brokeredmessage)
            {
                    if (brokeredmessage == null)
                    {
                        return;
                    }
                    if (brokeredmessage.Properties.ContainsKey("type") && brokeredmessage.Properties["type"].ToString() == MessageType.MessageCollection.ToString())
                    {

                        var message = brokeredmessage.ToMonitorMessage();
                        new DataManager().UpdateRow(message);
                    }
                    else
                    {
                        new ErrorRecorder().WriteError( new Exception("Unrecodnised Message"),brokeredmessage);
                    }
                }
            }
        }
    
