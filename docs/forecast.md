# Forecast Data API

Get irradiance, weather and power forecasts from the present time up to 14 days ahead for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas, nowcasted for approx. four hours ahead) and numerical weather models (other data and longer horizons).

---


The module ForecastClient has the following available methods:

| Endpoint                  | Purpose                                              | API Docs                                                                                                               |
|---------------------------|------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------|
| [GetRadiationAndWeather](#getradiationandweather) | Get irradiance and weather forecasts for the requested location from the present up to 14 days ahead, derived from satellite (clouds and irradiance over non-polar continental areas, now casted for approx. four hours ahead) and numerical weather models (other data and longer horizons). | [details](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1){.md-button} |
| [GetRooftopPvPower](#getrooftoppvpower) | Get basic rooftop PV power forecasts from the present time up to 14 days ahead for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas, nowcasted for approx. four hours ahead) and numerical weather models (other data and longer horizons).          The basic rooftop power simulation is only suitable for residential and smaller C&I rooftop sites, not for grid-scale sites.          **Attention hobbyist users**          If you have a hobbyist user account please use the [Rooftop Sites (Hobbyist)](https://docs.solcast.com.au/#00577cf8-b43b-4349-b4b5-a5f063916f5a) endpoints. | [details](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51){.md-button} |
| [GetAdvancedPvPower](#getadvancedpvpower) | Get high-spec PV power forecasts for the requested site from the present up to 14 days ahead, derived from satellite (clouds and irradiance over non-polar continental areas, nowcasted for approx. four hours ahead) and numerical weather models (other data and longer horizons). | [details](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0){.md-button} |

---

### GetRadiationAndWeather
**Parameters:**
[latitude](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1 "(double?): The latitude of the location you request data for. Must be a decimal number between -90 and 90. (Required)"), [longitude](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1 "(double?): The longitude of the location you request data for. Must be a decimal number between -180 and 180. (Required)"), [hours](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1 "(int?): The number of hours to return in the response. (Optional)"), [period](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1 "(string): Length of the averaging period in ISO 8601 format. (Optional)"), [tilt](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1 "(float?): The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90. (Optional)"), [azimuth](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1 "(float?): The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere. (Optional)"), [arrayType](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1 "(string): The type of sun-tracking or geometry configuration of your site's modules. (Optional)"), [outputParameters](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1 "(List<string>): The output parameters to include in the response. (Optional)"), [terrainShading](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1 "(bool?): If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects. (Optional)"), [format](https://docs.solcast.com.au/#4e0e8a96-7a12-4654-8407-6bbbb37478b1 "(string): Response format (Optional)")

**Example Usage:**
```csharp
using Solcast.Clients;

var forecastClient = new ForecastClient();
var response = await forecastClient.GetRadiationAndWeather(
    latitude: -33.856784,
    longitude: 151.215297,
    period: "PT30M",
    tilt: 30.0f,
    azimuth: 180.0f,
    format: "csv"
);
Console.WriteLine(response.RawResponse);

```
**Sample Output:**

| air_temp | dni | ghi | period_end | period |
| --- | --- | --- | --- | --- |
| 30 | 847 | 1001 | 2024-11-27T02:00:00Z | PT30M |
| 30 | 938 | 1026 | 2024-11-27T02:30:00Z | PT30M |
| ... | ... | ... | ... | ... |
| 21 | 0 | 222 | 2024-11-29T01:30:00Z | PT30M |
| 21 | 0 | 226 | 2024-11-29T02:00:00Z | PT30M |

---

### GetRooftopPvPower
**Parameters:**
[latitude](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(double?): The latitude of the location you request data for. Must be a decimal number between -90 and 90. (Required)"), [longitude](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(double?): The longitude of the location you request data for. Must be a decimal number between -180 and 180. (Required)"), [capacity](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(float?): The capacity of the inverter (AC) or the modules (DC), whichever is greater, in kilowatts (kW). (Required)"), [hours](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(int?): The number of hours to return in the response. (Optional)"), [period](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(string): Length of the averaging period in ISO 8601 format. (Optional)"), [tilt](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(float?): The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90. (Optional)"), [azimuth](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(float?): The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere. (Optional)"), [installDate](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(string): The date (yyyy-MM-dd) of installation of the PV system. We use this to estimate your loss_factor based on the ageing of your system. If you provide us with a loss_factor directly, we will ignore this date. (Optional)"), [lossFactor](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(float?): Default is 0.90 A factor to reduce your output forecast from the full capacity based on characteristics of the PV array or inverter. This is effectively the non-temperature loss effects on the nameplate rating of the PV system, including inefficiency and soiling. For a 1kW PV system anything that reduces 1000W/m2 solar radiation from producing 1000W of power output (assuming temperature is 25C). Valid values are between 0 and 1 (i.e. 0.6 equals 60%). If you specify 0.6 your returned power will be a maximum of 60% of AC capacity. (Optional)"), [outputParameters](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(List<string>): The output parameters to include in the response. (Optional)"), [terrainShading](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(bool?): If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects. (Optional)"), [format](https://docs.solcast.com.au/#155071c9-3457-47ea-a689-88fa894b0f51 "(string): Response format (Optional)")

**Example Usage:**
```csharp
using Solcast.Clients;

var forecastClient = new ForecastClient();
var response = await forecastClient.GetRooftopPvPower(
    latitude: -33.856784,
    longitude: 151.215297,
    capacity: 5.0f,
    format: "csv"
);
Console.WriteLine(response.RawResponse);

```
**Sample Output:**

| pv_power_rooftop | period_end | period |
| --- | --- | --- |
| 3.694 | 2024-11-27T02:00:00Z | PT30M |
| 3.772 | 2024-11-27T02:30:00Z | PT30M |
| ... | ... | ... |
| 0.864 | 2024-11-29T01:30:00Z | PT30M |
| 0.878 | 2024-11-29T02:00:00Z | PT30M |

---

### GetAdvancedPvPower
**Parameters:**
[resourceId](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(string): The resource id of the resource. (Required)"), [hours](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(int?): The number of hours to return in the response. (Optional)"), [outputParameters](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(List<string>): The output parameters to include in the response. (Optional)"), [period](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(string): Length of the averaging period in ISO 8601 format. (Optional)"), [applyAvailability](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(double?): Percentage of the site’s total AC (inverter) capacity that is currently generating or expected to be generating during the forecast request period. E.g. if you specify a 50% availability, your returned power will be half of what it otherwise would be. (Optional)"), [applyConstraint](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(double?): Constraint on site’s total AC production, applied as a cap in the same way as the metadata parameter Site Export Limit. This will constrain all Solcast power values to be no higher than the apply_constraint value you specify. If you need an unconstrained forecast, you should not use this parameter. (Optional)"), [applyDustSoiling](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(double?): A user-override for dust_soiling_average. If you specify this parameter in your API call, we will replace the site's annual or monthly average dust soiling values with the value you specify in your API call.E.g. if you specify a 0.7 dust soiling, your returned power will be reduced by 70%. (Optional)"), [applySnowSoiling](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(double?): A user-override for Solcast’s dynamic snow soiling, which is based on global snow cover and weather forecast data, and changes from hour to hour. If you specify this parameter in your API call (e.g. if snow clearing has just been performed), we will replace the Solcast dynamic hour to hour value with the single value you specify. E.g. if you specify a 0.7 snow soiling, your returned power will be reduced by 70%. (Optional)"), [applyTrackerInactive](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(bool?): Indicating if trackers are inactive. If True, panels are assumed all facing up (i.e. zero rotation). Only has effect if your site has a tracking_type that is not “fixed”. (Optional)"), [terrainShading](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(bool?): If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects. (Optional)"), [format](https://docs.solcast.com.au/#c68d40a1-930b-468a-afda-e2c50ae2f6b0 "(string): Response format (Optional)")

**Example Usage:**
```csharp
using Solcast.Clients;

var forecastClient = new ForecastClient();
var response = await forecastClient.GetAdvancedPvPower(
    resourceId: "ba75-e17a-7374-95ed",
    format: "csv"
);
Console.WriteLine(response.RawResponse);

```
**Sample Output:**

| pv_power_advanced | period_end | period |
| --- | --- | --- |
| 10 | 2024-11-27T02:00:00Z | PT30M |
| 10 | 2024-11-27T02:30:00Z | PT30M |
| ... | ... | ... |
| 9.573 | 2024-12-04T01:30:00Z | PT30M |
| 9.45 | 2024-12-04T02:00:00Z | PT30M |

---
