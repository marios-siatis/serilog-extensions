FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MariosSiati.StructuredLogging.WebAppSample.csproj", "StructuredLogging/"]
RUN dotnet restore "StructuredLogging/MariosSiati.StructuredLogging.WebAppSample.csproj"
COPY . .
WORKDIR "/src/StructuredLogging"
RUN dotnet build "MariosSiati.StructuredLogging.WebAppSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MariosSiati.StructuredLogging.WebAppSample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StructuredLogging.dll"]
