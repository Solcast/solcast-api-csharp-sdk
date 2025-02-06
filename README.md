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
![Alt text for the image](examples/csharp.svg)

### Retrieving Live Radiation and Weather Data
```csharp
using Solcast.Clients;

var liveClient = new LiveClient();
var response = await liveClient.GetRadiationAndWeather(
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
var response = await forecastClient.GetForecast(
    latitude: -33.856784,
    longitude: 151.215297,
    outputParameters: ["air_temp", "dni", "ghi"]
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
- `GetAdvancedPvPower`: Retrieves advanced PV power live data.
- `GetRooftopPvPower`: Retrieves live rooftop PV power data based on location and other parameters.
### ForecastClient
- `GetForecast`: Retrieves forecast solar radiation and weather data for up to 14 days ahead.
- `GetRadiationAndWeather`: Retrieves forecast radiation and weather data for a specified location.
- `GetAdvancedPvPower`: Retrieves advanced PV power forecasts with customizable options.
- `GetRooftopPvPower`: Retrieves rooftop PV power forecast data based on location and other parameters.
### HistoricClient
- `GetRadiationAndWeather`: Retrieves historic solar radiation and weather data for a specified time range.
- `GetAdvancedPvPower`: Retrieves advanced PV power historical data.
- `GetRooftopPvPower`: Retrieves rooftop PV power historical data.
### TmyClient
- `GetRadiationAndWeather`: Retrieves TMY irradiance and weather data for a specified location.
- `GetAdvancedPvPower`: Retrieves advanced PV power TMY data.
- `GetRooftopPvPower`: Retrieves TMY rooftop PV power data.
### AggregationClient
- `GetLiveAggregation`: Retrieves live grid aggregation data for up to 7 days.
- `GetForecastAggregation`: Retrieves forecast grid aggregation data for up to 7 days.
### PvPowerSitesClient
- `GetPvPowerSites`: Retrieves a list of all available PV power sites.
- `GetPvPowerSite`: Retrieves metadata for a specific PV power site by its resource ID.
- `PostPvPowerSite`: Creates a new PV Power Site for use with advanced PV power model.
- `PatchPvPowerSite`: Partially updates the specifications of an existing PV power site.
- `PutPvPowerSite`: Overwrites an existing PV power site specifications.
- `DeletePvPowerSite`: Deletes an existing PV power site.

## Optional: Suppressing SDK Update Checks
To suppress the SDK's automatic update check, set the SUPPRESS_SDK_UPDATE_CHECK environment variable to true:

Windows PowerShell:
```powershell
$env:SUPPRESS_SDK_UPDATE_CHECK = "true"
```

Linux/macOS:
```bash
export SUPPRESS_SDK_UPDATE_CHECK="true"
```
When this flag is set, the SDK will skip checking for new versions during runtime. This is particularly useful automated environments where update messages are not necessary.

## Contributing
We welcome contributions to this SDK! If you'd like to contribute, please submit a Pull Request or open an issue with any suggestions or bug reports.

To generate the sdk from the openapi specs run:
```bash
python3 generate_sdk_csharp.py
```

To generate the documentation run:
```bash
python3 generate_sdk_docs.py [--use-cache]
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
