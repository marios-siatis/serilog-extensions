# Serilog & SEQ Wrapper 

Replaces normal Microsoft Extensions Logging (MEL) with Serilog.

### Usage

Use the following extension method on ICollectionService on Program Startup:

```C#
UseSerilog(applicationName)
```

#### AppSettings

### Samples

WebApp Logging:
<img width="1025" alt="image" src="https://user-images.githubusercontent.com/85685549/200656977-22b59237-0fd8-4edb-9ed9-68a6a4f1cad2.png">

Function App Logging:
<img width="1025" alt="image" src="https://user-images.githubusercontent.com/85685549/200911432-3e83a9fe-5379-4de1-acff-4ab6b5abda2b.png">

### Compatibility 

.Net Core 6.0
.Net Functions Worker v4
