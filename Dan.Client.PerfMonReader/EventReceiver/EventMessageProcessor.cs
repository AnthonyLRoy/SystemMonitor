// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventMessageProcessor.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The event message processor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace EventReceiver
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net;
    using System.Threading;

    using EventReceiver.Processors;

    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;

    /// <summary>
    /// The event message processor.
    /// </summary>
    public class EventMessageProcessor : RoleEntryPoint
    {
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
            string queueName = CloudConfigurationManager.GetSetting("Monitor.Queue");

            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(queueName))
            {
                namespaceManager.CreateQueue(queueName);
            }

            // Initialize the connection to Service Bus Queue
            this.Client = QueueClient.CreateFromConnectionString(connectionString, queueName);
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
            var options = new OnMessageOptions { AutoComplete = true, MaxConcurrentCalls = 5 };
            options.ExceptionReceived += this.options_ExceptionReceived;

            this.Client.OnMessage(
                receivedMessage =>
                    {
                        try
                        {
                            Trace.WriteLine(
                                "Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString() + " - "
                                + DateTime.Now);
                            new EventProcessor().Process(receivedMessage);
                            Trace.WriteLine(
                                "Finished processing Service Bus message: "
                                + receivedMessage.SequenceNumber.ToString(CultureInfo.InvariantCulture) + " - "
                                + DateTime.Now);
                        }
                        catch (Exception e)
                        {
                            new ErrorRecorder().WriteError(e);
                        }
                    }, 
                options);

            this.CompletedEvent.WaitOne();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The options_ exception received.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void options_ExceptionReceived(object sender, ExceptionReceivedEventArgs e)
        {
            // Write the error messaget to the Database for investigates
            new ErrorRecorder().WriteError(e.Exception);
        }

        #endregion
    }
}