<img src="docs/img/logo.png" width="100" align="right">

# Solcast API C# SDK

[![Build, Tests](https://github.com/solcast/solcast_net/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/solcast/solcast_net/actions/workflows/build-and-test.yml)
[![Docs](https://github.com/solcast/solcast_net/actions/workflows/deploy-docs.yml/badge.svg)](https://github.com/solcast/solcast_net/actions/workflows/deploy-docs.yml)

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

- C# SDK documentation: https://solcast.github.io/solcast_net/
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
Before using the SDK, you need to set your Solcast API key as an environment variable. You can register for an API key at Solcast Toolkit.

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
### Retrieving Live Radiation and Weather Data
```csharp
using Solcast.Clients;

var liveClient = new LiveClient();
var response = await liveClient.GetRadiationAndWeather(
    latitude: -33.856784,
    longitude: 151.215297,
    output_parameters: ["air_temp", "dni", "ghi"]
    format: "csv"
);

Console.WriteLine(response);
```

### Retrieving Forecast Data
```csharp
using Solcast.Clients;

var forecastClient = new ForecastClient();
var response = await forecastClient.GetForecast(
    latitude: -33.856784,
    longitude: 151.215297,
    output_parameters: ["air_temp", "dni", "ghi"]
);
```

### Retrieving Historic Radiation and Weather Data
```csharp
using Solcast.Clients;

var historicClient = new HistoricClient();
var response = await historicClient.GetRadiationAndWeather(
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
var response = await tmyClient.GetRadiationAndWeather(
    latitude: -33.856784,
    longitude: 151.215297,
);
```

### Retrieving Grid Aggregation Forecast Data
```csharp
using Solcast.Clients;

var aggregationClient = new AggregationClient();
var response = await aggregationClient.GetForecastAggregation(
    collectionId: "country_total",
    aggregationId: "it_total",
    outputParameters: ["percentage", "pv_estimate"],
    format: "csv"
);
```

#### Listing all available PV Power Sites:
```csharp
using Solcast.Clients;

var pvClient = new PvPowerSitesClient();
var response = await pvClient.ListPvPowerSites();
```

#### Getting metadata of a specific PV Power Site:
```csharp
using Solcast.Clients;

var pvClient = new PvPowerSitesClient();
var response = await pvClient.GetPvPowerSite("ba75-e17a-7374-95ed");
```

## API Methods
### LiveClient
- `GetRadiationAndWeather`: Retrieves live solar radiation and weather data.
### ForecastClient
- `GetForecast`: Retrieves forecast solar radiation and weather data for up to 14 days ahead.
### HistoricClient
- `GetRadiationAndWeather`: Retrieves historic solar radiation and weather data.
### TmyClient
- `GetRadiationAndWeather`: Retrieves TMY (Typical Meteorological Year) irradiance and weather data.
- `GetRooftopPvPower`: Retrieves TMY rooftop PV power estimated actuals.
### AggregationClient
- `GetLiveAggregation`: Retrieves live grid aggregation data for up to 7 days.
- `GetForecastAggregation`: Retrieves forecast grid aggregation data for up to 7 days.
### PvPowerSitesClient
- `ListPvPowerSites`: Retrieves a list of available PV Power Sites.
- `GetPvPowerSite`: Retrieves metadata for a specific PV Power Site.
- `CreatePvPowerSite`: Creates a new PV Power Site for use with advanced solar power modeling.
- `PatchPvPowerSite`: Partially updates the metadata for an existing PV Power Site.
- `UpdatePvPowerSite`: Overwrites the metadata for an existing PV Power Site.
- `DeletePvPowerSite`: Deletes an existing PV Power Site.


## Contributing
We welcome contributions to this SDK! If you'd like to contribute, please submit a Pull Request or open an issue with any suggestions or bug reports.

To generate the sdk from the openapi specs run:
```bash
python3 generate_sdk_csharp.py
```

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
