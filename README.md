# Solcast API C# SDK ![](https://raw.githubusercontent.com/Solcast/solcast-api-csharp-sdk/main/docs/img/logo_s.png)


[![Build, Test, Package](https://github.com/solcast/solcast-api-csharp-sdk/actions/workflows/build-test-package.yml/badge.svg)](https://github.com/solcast/solcast-api-csharp-sdk/actions/workflows/build-test-package.yml) [![Docs](https://github.com/solcast/solcast-api-csharp-sdk/actions/workflows/deploy-docs.yml/badge.svg)](https://github.com/solcast/solcast-api-csharp-sdk/actions/workflows/deploy-docs.yml)

A **C# SDK** to access the **Solcast API**, allowing you to retrieve solar radiation, weather data, and forecasts from satellite and numerical models.

---

## Features

- Retrieve live solar radiation and weather data.
- Access forecast data up to 14 days in advance.
- Access Typical Meteorological Year (TMY) data for irradiance and rooftop PV power.
- Get live and forecast grid aggregation data.
- Manage PV Power Sites for advanced solar power modeling.
- Simple, easy-to-use client classes for API interaction.

---

## Documentation

- C# SDK documentation: https://solcast.github.io/solcast-api-csharp-sdk/
- Full API documentation available at: [Solcast API Docs](https://docs.solcast.com.au)

---

## Installation

Install the SDK via **NuGet** with the following command:

```bash
dotnet add package Solcast
```

Alternatively, you can build the SDK locally by cloning the repository and running the build command:

```bash
git clone https://github.com/Solcast/solcast-api-csharp-sdk.git
cd solcast-api-csharp-sdk
dotnet build
```

---
## Configuration

Before using the SDK, you need to set your Solcast API key as an environment variable. You can register for an API key at [Solcast Toolkit](https://toolkit.solcast.com.au/).

To set the API key in your environment:

Windows PowerShell:
```powershell
$env:SOLCAST_API_KEY = "{your_api_key}"
```

Linux/macOS:
```bash
export SOLCAST_API_KEY="{your_api_key}"
```

## Basic Usage
![Alt text for the image](examples/csharp.svg)

### Retrieving Live Radiation and Weather Data
```csharp
using Solcast.Clients;

var liveClient = new LiveClient();
var response = await liveClient.GetLiveRadiationAndWeather(
    latitude: -33.856784,
    longitude: 151.215297,
    outputParameters: ["air_temp", "dni", "ghi"]
    format: "csv"
);

Console.WriteLine(response.RawResponse);
```

### Retrieving Forecast Data
```csharp
using Solcast.Clients;

var forecastClient = new ForecastClient();
var response = await forecastClient.GetForecastRadiationAndWeather(
    latitude: -33.856784,
    longitude: 151.215297,
    outputParameters: ["air_temp", "dni", "ghi"]
);
```

### Retrieving Historic Radiation and Weather Data
```csharp
using Solcast.Clients;

var historicClient = new HistoricClient();
var response = await historicClient.GetHistoricRadiationAndWeather(
    latitude: -33.856784,
    longitude: 151.215297,
    start: "2022-01-01T00:00",
    duration: "P1D"
);
```

### Retrieving TMY Radiation and Weather Data
```csharp
using Solcast.Clients;

var tmyClient = new TmyClient();
var response = await tmyClient.GetTmyRadiationAndWeather(
    latitude: -33.856784,
    longitude: 151.215297,
);
```

### Retrieving Grid Aggregation Forecast Data
```csharp
using Solcast.Clients;

var aggregationClient = new AggregationClient();
var response = await aggregationClient.GetForecastAggregations(
    collectionId: "country_total",
    aggregationId: "it_total",
    outputParameters: ["percentage", "pv_estimate"],
    format: "csv"
);
```

#### Listing all available PV Power Sites:
```csharp
using Solcast.Clients;

var pvClient = new PvPowerSiteClient();
var response = await pvClient.GetPvPowerSites();
```

#### Getting metadata of a specific PV Power Site:
```csharp
using Solcast.Clients;

var pvClient = new PvPowerSiteClient();
var response = await pvClient.GetPvPowerSite("ba75-e17a-7374-95ed");
```

## API Methods
### LiveClient
- `GetLiveRadiationAndWeather`: Retrieves live solar radiation and weather data.
- `GetLiveAdvancedPvPower`: Retrieves advanced PV power live data.
- `GetLiveRooftopPvPower`: Retrieves live rooftop PV power data based on location and other parameters.
### ForecastClient
- `GetForecastRadiationAndWeather`: Retrieves irradiance and weather forecasts for the requested location from the present up to 14 days ahead
- `GetForecastAdvancedPvPower`: Retrieves advanced PV power forecasts with customizable options.
- `GetForecastRooftopPvPower`: Retrieves rooftop PV power forecast data based on location and other parameters.
### HistoricClient
- `GetHistoricRadiationAndWeather`: Retrieves historic solar radiation and weather data for a specified time range.
- `GetHistoricAdvancedPvPower`: Retrieves advanced PV power historical data.
- `GetHistoricRooftopPvPower`: Retrieves rooftop PV power historical data.
### TmyClient
- `GetTmyRadiationAndWeather`: Retrieves TMY irradiance and weather data for a specified location.
- `GetTmyAdvancedPvPower`: Retrieves advanced PV power TMY data.
- `GetTmyRooftopPvPower`: Retrieves TMY rooftop PV power data.
### AggregationClient
- `GetLiveAggregations`: Retrieves live grid aggregation data for up to 7 days.
- `GetForecastAggregations`: Retrieves forecast grid aggregation data for up to 7 days.
### PvPowerSiteClient
- `GetPvPowerSites`: Retrieves a list of all available PV power sites.
- `GetPvPowerSite`: Retrieves metadata for a specific PV power site by its resource ID.
- `PostPvPowerSite`: Creates a new PV Power Site for use with advanced PV power model.
- `PatchPvPowerSite`: Partially updates the specifications of an existing PV power site.
- `PutPvPowerSite`: Overwrites an existing PV power site specifications.
- `DeletePvPowerSite`: Deletes an existing PV power site.

## Optional: Enabling SDK Update Checks
By default, the SDK does not check for updates during runtime. To enable automatic update checking, you can pass `true` for the `checkForUpdates` parameter when creating client instances:

```csharp
var liveClient = new LiveClient(checkForUpdates: true);
```

Alternatively, to enable update checking automatically for all client instances, set the CHECK_SDK_UPDATE environment variable to true:

Windows PowerShell:
```powershell
$env:CHECK_SDK_UPDATE = "true"
```

Linux/macOS:
```bash
export CHECK_SDK_UPDATE="true"
```
When this flag is set, the SDK will automatically check for new versions during runtime regardless of the `checkForUpdates` parameter value.

## Running Tests
To run the tests, use the following command:
```bash
dotnet build && dotnet test
```

## Executing examples:
Live:
```bash
dotnet run --project examples/Solcast.Examples/Solcast.Examples.csproj live
```

Historic:
```bash
dotnet run --project examples/Solcast.Examples/Solcast.Examples.csproj historic
```

Forecast:
```bash
dotnet run --project examples/Solcast.Examples/Solcast.Examples.csproj forecast
```


---

## License
This SDK is licensed under the Apache 2.0 License. See the [LICENSE](LICENSE) file for more information.
