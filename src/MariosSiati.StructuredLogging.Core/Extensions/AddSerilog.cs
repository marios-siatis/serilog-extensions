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
    public static IServiceCollection UseSerilog(this IServiceCollection services,
        string applicationName,
        bool useApplicationInsights = false,
        bool useLocalSeq = true,
        string correlationIdHeaderKey = "x-correlation-id")
    {
        var sp = services.BuildServiceProvider();
        var config = sp.GetRequiredService<IConfiguration>();

        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.WithProperty("ApplicationName", applicationName)
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationIdHeader(correlationIdHeaderKey)
            .Enrich.WithCorrelationId()
            .WriteTo.Console()
            .ReadFrom.Configuration(config);

        if (useApplicationInsights)
        {
            var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
            var client = new TelemetryClient(telemetryConfiguration);
            loggerConfiguration.WriteTo.ApplicationInsights(client, TelemetryConverter.Traces);
        }

        if (useLocalSeq)
        {
            services.UseLocalSeq(config, loggerConfiguration);
        }

        Log.Logger = loggerConfiguration.CreateLogger();

        services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddSerilog(Log.Logger);
        });

        return services;
    }

    private static void UseLocalSeq(this IServiceCollection services, IConfiguration config, LoggerConfiguration loggerConfiguration)
    {
#if DEBUG
        var seqOptions = config.GetSection(nameof(Seq));

        services.AddOptions<Seq>().BindConfiguration(nameof(Seq))
            .ValidateDataAnnotations();

        var seq = seqOptions.Get<Seq>();
        loggerConfiguration.WriteTo.Seq(seq.ServerUrl);
#endif
    }
}

public class Seq
{
    [Required] public string ServerUrl { get; set; }
}