# Solcast API C# SDK ![](https://raw.githubusercontent.com/Solcast/solcast-api-csharp-sdk/main/docs/img/logo_s.png)

The Solcast API provides forecast, live and historical solar irradiance, PV power and weather data.

## Features

- Retrieve live and forecast solar radiation and weather data
- Get live and forecast grid aggregation data
- Support for all Solcast Commercial products
- Manage PV Power Sites for advanced solar power modeling
- Simple, easy-to-use client classes for API interaction

## Installation

```bash
dotnet add package Solcast
```

## Configuration

Get an API key from the [Solcast API Toolkit](https://toolkit.solcast.com.au/), then set it:

```bash
export SOLCAST_API_KEY=your-api-key
```

## Getting started

This example calls the Sydney Opera House, one of Solcast's [unmetered locations](https://docs.solcast.com.au/docs/unmetered-locations), so you can run all examples without using your request quota.

```csharp
using Solcast;

var client = SolcastClientFactory.Create();

var response = await client.Data.Live.RadiationAndWeather.GetAsync(request =>
{
    request.QueryParameters.OutputParameters = ["air_temp", "dni", "ghi"];
    request.QueryParameters.Latitude = -33.856784;
    request.QueryParameters.Longitude = 151.215297;
});

foreach (var record in response!.EstimatedActuals!)
{
    Console.WriteLine($"{record.PeriodEnd}: {record.Ghi}");
}
```

Every field is typed and documents its own meaning and units, so `record.Ghi` explains
itself in IntelliSense. Fields you did not request are null.

## Errors

Every failure throws a `ResponseStatus` carrying the error code and the fields at fault.

```csharp
using Solcast.Models;

try
{
    var response = await client.Data.Live.RadiationAndWeather.GetAsync(request =>
{
    request.QueryParameters.OutputParameters = ["air_temp", "dni", "ghi"];
    request.QueryParameters.Latitude = -33.856784;
    request.QueryParameters.Longitude = 151.215297;
});
}
catch (ResponseStatus error)
{
    Console.Error.WriteLine($"{error.ResponseStatusCode} {error.ErrorCode}: {error.Message}");
    foreach (var field in error.Errors ?? [])
    {
        Console.Error.WriteLine($"  {field.FieldName}: {field.Message}");
    }
}
```

## CSV

`CsvWriter` turns records into CSV. It is a plain utility over the data and knows nothing about the
client, so it works with any response.

```csharp
using Solcast.Utilities;

File.WriteAllText("live.csv", CsvWriter.ToCsv(response!.EstimatedActuals!));
```

## Endpoint reference

Full documentation for every endpoint: <https://solcast.github.io/solcast-api-csharp-sdk/>

- Forecast
- Geographic
- Grid Aggregations
- Historic
- Live
- Resources
- Schedule
- TMY

The client is generated with [Kiota](https://learn.microsoft.com/openapi/kiota/)

## License

Apache 2.0. See the [LICENSE](LICENSE) file.
