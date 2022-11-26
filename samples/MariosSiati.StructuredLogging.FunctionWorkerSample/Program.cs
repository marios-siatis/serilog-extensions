using MariosSiati.StructuredLogging.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MariosSiati.StructuredLogging.FunctionWorkerSample
{
    public static class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(RegisterDependencies)
                .Build();

            host.Run();
        }

        private static void RegisterDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.UseSerilog(applicationName:"SampleFunctionWorker", useApplicationInsights:true);
            var sp = serviceCollection.BuildServiceProvider();
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            const string categoryName = "Any";
            var logger = loggerFactory.CreateLogger(categoryName);
            logger.LogDebug("Debug");
            logger.LogInformation("Information");
            logger.LogWarning("Warning");
            logger.LogError("Error");
        }
    }
}