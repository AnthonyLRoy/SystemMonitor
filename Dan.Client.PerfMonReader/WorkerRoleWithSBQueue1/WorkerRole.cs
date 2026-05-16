// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerRole.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The worker role.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WorkerRoleWithSBQueue1
{
    using System.Diagnostics;
    using System.Net;
    using System.Threading;

    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;

    /// <summary>
    /// The worker role.
    /// </summary>
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        #region Constants

        /// <summary>
        /// The queue name.
        /// </summary>
        private const string QueueName = "ProcessingQueue";

        #endregion

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        #region Fields

        /// <summary>
        /// The client.
        /// </summary>
        private QueueClient Client;

        /// <summary>
        /// The completed event.
        /// </summary>
        private ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on start.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Create the queue if it does not exist already
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Initialize the connection to Service Bus Queue
            this.Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);
            return base.OnStart();
        }

        /// <summary>
        /// The on stop.
        /// </summary>
        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            this.Client.Close();
            this.CompletedEvent.Set();
            base.OnStop();
        }

        /// <summary>
        /// The run.
        /// </summary>
        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            // Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
            this.Client.OnMessage(
                receivedMessage =>
                    {
                        try
                        {
                            // Process the message
                            Trace.WriteLine(
                                "Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());
                        }
                        catch
                        {
                            // Handle any message processing specific exceptions here
                        }
                    });

            this.CompletedEvent.WaitOne();
        }

        #endregion
    }
}