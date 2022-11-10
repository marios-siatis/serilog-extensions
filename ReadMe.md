# 🪵 Serilog & SEQ Wrapper 

This package is useful in cases where the project is already existing and we don't want refactor each and every file to use serilog instead of MEL logger
- Sets up structure logging by replacing normal Microsoft Extensions Logging (MEL) with Serilog.
- Enriches the log context with the Application Name. Useful when working locally with multiple services.
- Writes logs to Applications Insights 
- Writes logs local SEQ instance. 

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
