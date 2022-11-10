# 🪵 Serilog & SEQ Wrapper 

This package offers an easy setup of serilog, with just a single line of code.

It is also useful in existing projects, where it would be an overhead to refactor each and every file to use serilog instead of MEL logger

- Sets up structured logging by replacing normal Microsoft Extensions Logging (MEL) with Serilog.
- Enriches the log context with the Application Name. Useful when working locally with multiple services.
- Writes logs to Applications Insights 
- Writes logs to local SEQ instance for debugging. 

The inspiration behind this package was the lack of structured logging support in Azure Functions V4 from MEL (especially logging custom properties in the log context) [more here](https://medium.com/@marios.shiatis/addressing-the-structured-logging-application-insights-issues-in-azure-functions-v4-net-6-0-f1f63b99807d)

https://www.nuget.org/packages/MariosSiati.StructuredLogging.Core/

### Usage

Use the following extension method on IServiceCollection on app Startup:

```C#
UseSerilog(applicationName)
```

#### AppSettings
```json
"seq": {
    "ServerUrl": "http://localhost:5341",
    "ApiKey": "1234567890",
    "MinimumLevel": "Trace",
    "LevelOverride": {
      "Microsoft": "Debug"
    }
  }
````
### Samples

WebApp Logging:
<img width="1025" alt="image" src="https://user-images.githubusercontent.com/85685549/200656977-22b59237-0fd8-4edb-9ed9-68a6a4f1cad2.png">

Function App Logging:
<img width="1025" alt="image" src="https://user-images.githubusercontent.com/85685549/200911432-3e83a9fe-5379-4de1-acff-4ab6b5abda2b.png">

### Compatibility 

.Net Core 6.0
.Net Functions Worker v4
