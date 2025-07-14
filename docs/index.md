# Welcome to Solcast

C# SDK that wraps [Solcast's API](https://docs.solcast.com.au/).

## Install

To install the SDK, add the package to your project using the following command:

```bash
dotnet add package Solcast
```

## Usage
!!! warning 

    To access Solcast data you will need a [commercial API key](https://toolkit.solcast.com.au/register). If you have the API key already, you can use it with this library either as an environment variable called SOLCAST_API_KEY, or you can pass it as an argument `api_key` when you call one of the library's methods. 

Fetching Live Radiation and Weather Data:
```csharp
using Solcast.Clients;

var liveClient = new LiveClient();
var response = await liveClient.GetLiveRadiationAndWeather(
    latitude: -33.856784,
    longitude: 151.215297,
    outputParameteres: ["air_temp", "dni", "ghi" ]
);
```

!!! tip

    When testing or developing, you should use `solcast.unmetered_locations` so that your usage isn't counted against your plan.

[Unmetered Locations](https://docs.solcast.com.au/#unmetered-locations) still require you to signup for a commercial API key (above).
```csharp
using Solcast.Clients;

var location = UnmeteredLocations.Locations["Sydney Opera House"];

var forecastClient = new ForecastClient();

var response = await forecastClient.GetForecastRooftopPvPower(
    latitude: location.Latitude,
    longitude: location.Longitude,
    outputParameters: ["pv_power_rooftop"],
    period: "PT5M",
    capacity: 5,
    tilt: 22,
    format: "csv"
);
```


For more detailed documentation, visit the following pages:

| Module           | API Docs                                 |
|------------------|------------------------------------------|
| `Live`           | [Solcast.Clients.LiveClient](live.md) |
| `Historic`       | [Solcast.Clients.HistoricClient](historic.md) |
| `Forecast`       | [Solcast.Clients.ForecastClient](forecast.md) |
| `TMY`            | [Solcast.Clients.TmyClient](tmy.md) |
| `PV Power Sites` | [Solcast.Clients.PvPowerSiteClient](pvpowersite.md) |
| `Aggregations`   | [Solcast.Clients.AggregationClient](aggregation.md) |


## API Documentation
For more detailed information about the Solcast API, please visit the official [API documentation](http://docs.solcast.com.au).

