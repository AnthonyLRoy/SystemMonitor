
using Topshelf;

namespace RemoteReader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            HostFactory.Run(x =>
            {
                x.Service<RemoteReaderService>(s =>
                {
                    s.ConstructUsing(name => new RemoteReaderService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Remote monitor Reader Service");
                x.SetDisplayName("DAN");
                x.SetServiceName("DAN");
            }); 

 
        }
    }
}
