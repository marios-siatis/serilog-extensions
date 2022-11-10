using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MariosSiati.StructuredLogging.Core.Extensions;

public static class AddSerilog
{
    public static IWebHostBuilder UseSerilog(this IWebHostBuilder builder, string applicationName)
    {
        var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
        var client = new TelemetryClient(telemetryConfiguration);

        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.ApplicationInsights(client, TelemetryConverter.Traces)
            .Enrich.WithProperty("ApplicationName", applicationName)
            .WriteTo.Console();

        builder.ConfigureServices((Action<WebHostBuilderContext, IServiceCollection>)((context, services) =>
                {
#if DEBUG
                    var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
                    var seqOptions =
                        config.GetSection(nameof(Seq));

                    services.AddOptions<Seq>().BindConfiguration(nameof(Seq))
                        .ValidateDataAnnotations();

                    var seq = seqOptions.Get<Seq>();
                    loggerConfiguration.WriteTo.Seq(seq.ServerUrl);

#endif
                    services.AddLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddSerilog(Log.Logger);
                    });
                }
            ));

        Log.Logger = loggerConfiguration.CreateLogger();
        return builder;
    }

    public static IServiceCollection UseSerilog(this IServiceCollection services, string applicationName)
    {
        var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
        var client = new TelemetryClient(telemetryConfiguration);

        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.ApplicationInsights(client, TelemetryConverter.Traces)
            .Enrich.WithProperty("ApplicationName", applicationName)
            .WriteTo.Console();

#if DEBUG
        var sp = services.BuildServiceProvider();
        var config = sp.GetRequiredService<IConfiguration>();
        var seqOptions = config.GetSection(nameof(Seq));

        services.AddOptions<Seq>().BindConfiguration(nameof(Seq))
            .ValidateDataAnnotations();

        var seq = seqOptions.Get<Seq>();
        loggerConfiguration.WriteTo.Seq(seq.ServerUrl);

#endif
        Log.Logger = loggerConfiguration.CreateLogger();

        services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddSerilog(Log.Logger);
        });

        return services;
    }
}

public class Seq
{
    [Required] public string ServerUrl { get; set; }
}