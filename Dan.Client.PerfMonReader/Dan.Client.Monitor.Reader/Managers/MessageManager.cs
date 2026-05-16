// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageManager.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The message manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Client.Monitor.Reader.Managers
{
    using Autofac;

    using Dan.Common.Messages;

    using ServiceBusClient;

    /// <summary>
    /// The message manager.
    /// </summary>
    public class MessageManager : IMessageManager
    {
        #region Fields

        /// <summary>
        /// The _service busservice.
        /// </summary>
        private readonly IMessageService _serviceBusservice = autofac.Container.Resolve<IMessageService>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The send messages.
        /// </summary>
        /// <param name="monitorMessageCollection">
        /// The monitor message collection.
        /// </param>
        public void SendMessages(MonitorMessageCollection monitorMessageCollection)
        {
            IMessage message = monitorMessageCollection.ToCompressedMessage();
            this._serviceBusservice.Publish(message);
        }

        #endregion
    }
}