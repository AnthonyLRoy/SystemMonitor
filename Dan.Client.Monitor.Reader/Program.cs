// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Client.Monitor.Reader
{
    using System.Configuration;
    using System.Reflection;

    using Autofac;

    using Dan.Client.Monitor.Reader.Managers;

    using log4net;

    using ServiceBusClient;

    using Topshelf;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        #region Static Fields

        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Methods

        /// <summary>
        /// The init.
        /// </summary>
        private static void Init()
        {
            RegisterAutofacContainer();
        }

        /// <summary>
        /// The main.
        /// </summary>
        private static void Main()
        {
            Log.Info("Preparing Service");
            Init();
            HostFactory.Run(
                x =>
                    {
                        x.Service<PerformanceMonitorManager>(
                            s =>
                                {
                                    s.ConstructUsing(name => new PerformanceMonitorManager());
                                    s.WhenStarted(tc => tc.Start());
                                    s.WhenStopped(tc => tc.Stop());
                                });
                        x.RunAsLocalSystem();
                        x.SetDescription("Read and Transmit performance counter information");
                        x.SetDisplayName("Performance counter reader service");
                        x.SetServiceName("Dan.Client.Monitor.Reader");
                    });
        }

        /// <summary>
        /// The register autofac container.
        /// </summary>
        private static void RegisterAutofacContainer()
        {
            var builder = new ContainerBuilder();
            var connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            var queueName = ConfigurationManager.AppSettings["QUEUE_NAME"];
            builder.RegisterType<MessageManager>().As<IMessageManager>();
            builder.RegisterInstance(new ServiceClient(connectionString, queueName)).As<IMessageService>();
            autofac.Container = builder.Build();
        }

        #endregion
    }
}