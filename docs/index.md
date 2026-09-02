# Solcast API

The Solcast API provides forecast, live and historical solar irradiance, PV power and weather data.

For full details of the API, see the [OpenAPI specification](https://dev-api.solcast.com.au/openapi)

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

This example calls the Sydney Opera House, one of Solcast's unmetered locations, so you can run it as
it stands without using your request quota.

```csharp
using Solcast;

var client = SolcastClientFactory.Create();

var response = await client.Data.Live.Radiation_and_weather.GetAsync(request =>
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

## Endpoint reference

- [Forecast](forecast.md)
- [Geographic](geographic.md)
- [Grid Aggregations](grid_aggregations.md)
- [Historic](historic.md)
- [Live](live.md)
- [Resources](resources.md)
- [Schedule](schedule.md)
- [TMY](tmy.md)
