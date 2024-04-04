using System.Diagnostics;
using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Core;

namespace Monitor{
    public static class MonitorService
{

    public static readonly string serviceName = Assembly.GetCallingAssembly().GetName().Name ?? "Unknown";
    public static TracerProvider TracerProvider;
    public static ActivitySource activitySource = new ActivitySource(serviceName);

    public static ILogger Log => Serilog.Log.Logger;
    
    static MonitorService()
    {
        //OpenTelemetry
        TracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddZipkinExporter(options =>
            {
                options.Endpoint = new Uri("http://localhost:9411/api/v2/spans"); // Your local Zipkin instance
            })
            .AddSource(activitySource.Name)
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
            .Build();

        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .WriteTo.Seq("http://seq:5341")
            .CreateLogger();
    }

    // Main method serving as the entry point
    public static void Main(string[] args)
    {
        Console.WriteLine("Monitoring service is running...");
    }
}

}
