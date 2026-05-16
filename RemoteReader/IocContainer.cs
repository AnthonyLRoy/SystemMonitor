
using Autofac;

public static class autofac
{
    public static IContainer Container { get; set; }

}

class Program
{

    static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterInstance(new MonitorCommunicationManager()).As<IMonitorCommunicationManager>();
        autofac.Container = builder.Build();

        PerformanceMonitorManager pm = new PerformanceMonitorManager();

    }
}