using MariosSiati.StructuredLogging.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.UseSerilog(applicationName: "WebAppSample");

var sp = builder.Services.BuildServiceProvider();
var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
const string categoryName = "Any";

var logger = loggerFactory.CreateLogger(categoryName);
logger.LogInformation("Information");
logger.LogWarning("Warning");
logger.LogDebug("Debug");
logger.LogError("Error");

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();