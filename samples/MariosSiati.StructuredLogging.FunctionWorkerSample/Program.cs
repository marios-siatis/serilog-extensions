using MariosSiati.StructuredLogging.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            serviceCollection.UseSerilog("SampleFunctionWorker");
        }
    }
}