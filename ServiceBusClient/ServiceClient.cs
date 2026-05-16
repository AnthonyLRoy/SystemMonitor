// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceClient.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The service client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ServiceBusClient
{
    using System.Collections.Generic;

    using Dan.Common.Messages;

    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    /// The service client.
    /// </summary>
    public class ServiceClient : IMessageService
    {
        #region Fields

        /// <summary>
        /// The _active connection.
        /// </summary>
        private bool _activeConnection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient"/> class.
        /// </summary>
        /// <param name="connectionstring">
        /// The connectionstring.
        /// </param>
        /// <param name="queueName">
        /// The queue name.
        /// </param>
        public ServiceClient(string connectionstring, string queueName)
        {
            this.Connectionstring = connectionstring;
            this.QueueName = queueName;
            this.InitClient();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        public QueueClient Client { get; set; }

        /// <summary>
        /// Gets or sets the connectionstring.
        /// </summary>
        public string Connectionstring { get; set; }

        /// <summary>
        /// Gets a value indicating whether is connection active.
        /// </summary>
        public bool IsConnectionActive
        {
            get
            {
                return this._activeConnection;
            }
        }

        /// <summary>
        /// Gets or sets the queue name.
        /// </summary>
        public string QueueName { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The publish.
        /// </summary>
        /// <param name="messages">
        /// The messages.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Publish(List<IMessage> messages)
        {
            return false;
        }

        /// <summary>
        /// The publish.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Publish(IMessage message)
        {
            var brokeredMessage = new BrokeredMessage(message.Body);
            foreach (var property in message.Properties)
            {
                brokeredMessage.Properties.Add(property.Key, property.Value);
            }

            this.Client.SendAsync(brokeredMessage);
            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The validate queues.
        /// </summary>
        /// <param name="namespaceManger">
        /// The namespace manger.
        /// </param>
        protected void ValidateQueues(NamespaceManager namespaceManger)
        {
            this._activeConnection = namespaceManger.QueueExists(this.QueueName);
        }

        /// <summary>
        /// The init client.
        /// </summary>
        private void InitClient()
        {
            var namespaceManger = NamespaceManager.CreateFromConnectionString(this.Connectionstring);
            this.ValidateQueues(namespaceManger);
            if (this._activeConnection)
            {
                this.Client = QueueClient.CreateFromConnectionString(this.Connectionstring, this.QueueName);
            }
        }

        #endregion
    }
}