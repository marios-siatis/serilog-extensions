using MariosSiati.StructuredLogging.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
builder.Services.UseSerilog(applicationName:"WebAppSample");
app.Run();