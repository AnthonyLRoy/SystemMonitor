// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrokeredMessageExtenstions.cs" company="DanSoft">
//   
// </copyright>
// <summary>
//   The brokered message extenstions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace EventReceiver
{
    using System.Collections.Generic;

    using Dan.Common.Messages;

    using Microsoft.ServiceBus.Messaging;

    using Newtonsoft.Json;

    /// <summary>
    /// The brokered message extenstions.
    /// </summary>
    public static class BrokeredMessageExtenstions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Converts the Received brokered Message to a Internal Monitor message
        /// </summary>
        /// <param name="brokeredMessage">
        /// The brokered message.
        /// </param>
        /// <returns>
        /// Returns a Monitor Message Collection <see cref="MonitorMessageCollection"/>.
        /// </returns>
        public static MonitorMessageCollection ToMonitorMessage(this BrokeredMessage brokeredMessage)
        {
            var message = new MonitorMessageCollection(true)
                            {
                                Properties = brokeredMessage.Properties, 
                                Messages =
                                    JsonConvert.DeserializeObject<List<MonitorMessage>>(
                                        brokeredMessage.GetBody<string>())
                            };
            return message;
        }

        #endregion
    }
}