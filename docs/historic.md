# Historic Data API

Historical irradiance, weather and power data, from 2007 to 7 days ago at 1-2 km and 5 minutes resolution.

---


The module HistoricClient has the following available methods:

| Endpoint                  | Purpose                                              | API Docs                                                                                                               |
|---------------------------|------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------|
| [GetHistoricRadiationAndWeather](#gethistoricradiationandweather) | Get historical irradiance and weather estimated actuals for up to 31 days of data at a time for a requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). Data is available from 2007-01-01T00:00Z up to real-time estimated actuals. | [details](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850){.md-button} |
| [GetHistoricRooftopPvPower](#gethistoricrooftoppvpower) | Get historical basic rooftop PV power estimated actuals for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data).          **Attention hobbyist users**          If you have a hobbyist user account please use the [Rooftop Sites (Hobbyist)](https://docs.solcast.com.au/#00577cf8-b43b-4349-b4b5-a5f063916f5a) endpoints. | [details](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6){.md-button} |
| [GetHistoricAdvancedPvPower](#gethistoricadvancedpvpower) | Get historical advanced PV power estimated actuals for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). | [details](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881){.md-button} |

---

### GetHistoricRadiationAndWeather
**Parameters:**
[start](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(string): ISO_8601 compliant starting datetime for the historical data. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed. (Required)"), [latitude](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(double?): The latitude of the location you request data for. Must be a decimal number between -90 and 90. (Required)"), [longitude](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(double?): The longitude of the location you request data for. Must be a decimal number between -180 and 180. (Required)"), [end](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(string): Must include one of end_date and duration. ISO_8601 compliant ending datetime for the historical data. Must be within 31 days of the start_date. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed. (Optional)"), [duration](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(string): Must include one of end_date and duration. ISO_8601 compliant duration for the historical data. Must be within 31 days of the start_date. (Optional)"), [timeZone](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(string): Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset. (Optional)"), [period](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(string): Length of the averaging period in ISO 8601 format. (Optional)"), [tilt](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(float?): The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90. (Optional)"), [azimuth](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(float?): The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere. (Optional)"), [arrayType](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(string): The type of sun-tracking or geometry configuration of your site's modules. (Optional)"), [outputParameters](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(List<string>): The output parameters to include in the response. (Optional)"), [terrainShading](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(bool?): If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects. (Optional)"), [format](https://docs.solcast.com.au/#f75aa7c6-b1ee-476c-9659-3bfb8bc7a850 "(string): Response format (Optional)")

**Example Usage:**
```csharp
using Solcast.Clients;

var historicClient = new HistoricClient();
var response = await historicClient.GetHistoricRadiationAndWeather(
    start: "2024-06-01T06:00",
    latitude: -33.856784,
    longitude: 151.215297,
    end: "2024-07-01T06:00",
    format: "csv"
);
Console.WriteLine(response.RawResponse);

```
**Sample Output:**

| air_temp | dni | ghi | period_end | period |
| --- | --- | --- | --- | --- |
| 15 | 0 | 5 | 2024-06-01T06:30:00+00:00 | PT30M |
| 15 | 0 | 1 | 2024-06-01T07:00:00+00:00 | PT30M |
| ... | ... | ... | ... | ... |
| 13 | 23 | 112 | 2024-07-01T05:30:00+00:00 | PT30M |
| 13 | 0 | 70 | 2024-07-01T06:00:00+00:00 | PT30M |

---

### GetHistoricRooftopPvPower
**Parameters:**
[start](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(string): ISO_8601 compliant starting datetime for the historical data. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed. (Required)"), [latitude](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(double?): The latitude of the location you request data for. Must be a decimal number between -90 and 90. (Required)"), [longitude](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(double?): The longitude of the location you request data for. Must be a decimal number between -180 and 180. (Required)"), [capacity](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(float?): The capacity of the inverter (AC) or the modules (DC), whichever is greater, in kilowatts (kW). (Required)"), [end](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(string): Must include one of end_date and duration. ISO_8601 compliant ending datetime for the historical data. Must be within 31 days of the start_date. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed. (Optional)"), [duration](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(string): Must include one of end_date and duration. ISO_8601 compliant duration for the historical data. Must be within 31 days of the start_date. (Optional)"), [timeZone](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(string): Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset. (Optional)"), [period](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(string): Length of the averaging period in ISO 8601 format. (Optional)"), [tilt](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(float?): The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90. (Optional)"), [azimuth](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(float?): The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere. (Optional)"), [installDate](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(string): The date (yyyy-MM-dd) of installation of the PV system. We use this to estimate your loss_factor based on the ageing of your system. If you provide us with a loss_factor directly, we will ignore this date. (Optional)"), [lossFactor](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(float?): Default is 0.90 A factor to reduce your output forecast from the full capacity based on characteristics of the PV array or inverter. This is effectively the non-temperature loss effects on the nameplate rating of the PV system, including inefficiency and soiling. For a 1kW PV system anything that reduces 1000W/m2 solar radiation from producing 1000W of power output (assuming temperature is 25C). Valid values are between 0 and 1 (i.e. 0.6 equals 60%). If you specify 0.6 your returned power will be a maximum of 60% of AC capacity. (Optional)"), [outputParameters](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(List<string>): The output parameters to include in the response. (Optional)"), [terrainShading](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(bool?): If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects. (Optional)"), [format](https://docs.solcast.com.au/#a3218b9c-ce7f-4fdd-850d-5f1029ae75f6 "(string): Response format (Optional)")

**Example Usage:**
```csharp
using Solcast.Clients;

var historicClient = new HistoricClient();
var response = await historicClient.GetHistoricRooftopPvPower(
    start: "2024-06-01T06:00",
    latitude: -33.856784,
    longitude: 151.215297,
    capacity: 5.0f,
    end: "2024-07-01T06:00",
    format: "csv"
);
Console.WriteLine(response.RawResponse);

```
**Sample Output:**

| pv_power_rooftop | period_end | period |
| --- | --- | --- |
| 0 | 2024-06-01T06:30:00+00:00 | PT30M |
| 0 | 2024-06-01T07:00:00+00:00 | PT30M |
| ... | ... | ... |
| 0.439 | 2024-07-01T05:30:00+00:00 | PT30M |
| 0.212 | 2024-07-01T06:00:00+00:00 | PT30M |

---

### GetHistoricAdvancedPvPower
**Parameters:**
[start](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(string): ISO_8601 compliant starting datetime for the historical data. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed. (Required)"), [resourceId](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(string): The resource id of the resource. (Required)"), [end](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(string): Must include one of end_date and duration. ISO_8601 compliant ending datetime for the historical data. Must be within 31 days of the start_date. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed. (Optional)"), [duration](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(string): Must include one of end_date and duration. ISO_8601 compliant duration for the historical data. Must be within 31 days of the start_date. (Optional)"), [timeZone](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(string): Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset. (Optional)"), [outputParameters](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(List<string>): The output parameters to include in the response. (Optional)"), [period](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(string): Length of the averaging period in ISO 8601 format. (Optional)"), [applyAvailability](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(double?): Percentage of the site’s total AC (inverter) capacity that is currently generating or expected to be generating during the forecast request period. E.g. if you specify a 50% availability, your returned power will be half of what it otherwise would be. (Optional)"), [applyConstraint](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(double?): Constraint on site’s total AC production, applied as a cap in the same way as the metadata parameter Site Export Limit. This will constrain all Solcast power values to be no higher than the apply_constraint value you specify. If you need an unconstrained forecast, you should not use this parameter. (Optional)"), [applyDustSoiling](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(double?): A user-override for dust_soiling_average. If you specify this parameter in your API call, we will replace the site's annual or monthly average dust soiling values with the value you specify in your API call.E.g. if you specify a 0.7 dust soiling, your returned power will be reduced by 70%. (Optional)"), [applySnowSoiling](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(double?): A user-override for Solcast’s dynamic snow soiling, which is based on global snow cover and weather forecast data, and changes from hour to hour. If you specify this parameter in your API call (e.g. if snow clearing has just been performed), we will replace the Solcast dynamic hour to hour value with the single value you specify. E.g. if you specify a 0.7 snow soiling, your returned power will be reduced by 70%. (Optional)"), [applyTrackerInactive](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(bool?): Indicating if trackers are inactive. If True, panels are assumed all facing up (i.e. zero rotation). Only has effect if your site has a tracking_type that is not “fixed”. (Optional)"), [terrainShading](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(bool?): If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects. (Optional)"), [format](https://docs.solcast.com.au/#359e01c2-ef0c-4f58-812f-47726b4c3881 "(string): Response format (Optional)")

**Example Usage:**
```csharp
using Solcast.Clients;

var historicClient = new HistoricClient();
var response = await historicClient.GetHistoricAdvancedPvPower(
    start: "2024-06-01T06:00",
    resourceId: "ba75-e17a-7374-95ed",
    end: "2024-07-01T06:00",
    format: "csv"
);
Console.WriteLine(response.RawResponse);

```
**Sample Output:**

| pv_power_advanced | period_end | period |
| --- | --- | --- |
| 0.023 | 2024-06-01T06:30:00+00:00 | PT30M |
| 0 | 2024-06-01T07:00:00+00:00 | PT30M |
| ... | ... | ... |
| 1.582 | 2024-07-01T05:30:00+00:00 | PT30M |
| 0.914 | 2024-07-01T06:00:00+00:00 | PT30M |

---
