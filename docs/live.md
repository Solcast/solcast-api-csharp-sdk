# Live

For full details, see https://docs.solcast.com.au/

| Endpoint | Purpose |
| --- | --- |
| [GetLiveAdvancedPvPower](#getliveadvancedpvpower) | Get high spec PV power estimated actuals for near real-time and past 7 days for the requested site, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). |
| [GetLiveHsuLosses](#getlivehsulosses) | Get soiling loss estimated actuals using the HSU model for near real-time and past 7 days for the requested location. |
| [GetLiveKimberLosses](#getlivekimberlosses) | Get soiling loss estimated actuals using the Kimber model for near real-time and past 7 days for the requested location. |
| [GetLiveRadiationAndWeather](#getliveradiationandweather) | Get irradiance and weather estimated actuals for near real-time and past 7 days for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). |
| [GetLiveRooftopPvPower](#getliverooftoppvpower) | Get basic rooftop PV power estimated actuals for near real-time and past 7 days for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). The basic rooftop power simulation is only suitable for residential and smaller C&I rooftop sites, not for grid-scale sites. |

## GetLiveAdvancedPvPower

Get high spec PV power estimated actuals for near real-time and past 7 days for the requested site, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data).

`GET /data/live/advanced_pv_power`

```csharp
var response = await client.Data.Live.AdvancedPvPower.GetAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| Hours | integer | No | The number of hours to return in the response. |
| OutputParameters | string[] | No | The output parameters to include in the response. Allowed values: pv_power_advanced. |
| ResourceId | string | Yes | The resource id of the resource. |
| Period | Period | No | Length of the averaging period in ISO 8601 duration format. Default is PT30M. |
| ApplyAvailability | number | No | Percentage of the site’s total AC (inverter) capacity that is currently generating or expected to be generating during the forecast request period. E.g. if you specify a 50% availability, your returned power will be half of what it otherwise would be. |
| ApplyConstraint | number | No | Constraint on site’s total AC production, applied as a cap in the same way as the metadata parameter Site Export Limit. This will constrain all Solcast power values to be no higher than the apply_constraint value you specify. If you need an unconstrained forecast, you should not use this parameter. |
| ApplyDustSoiling | number | No | A user-override for dust_soiling_average. If you specify this parameter in your API call, we will replace the site's annual or monthly average dust soiling values with the value you specify in your API call.E.g. if you specify a 0.7 dust soiling, your returned power will be reduced by 70%. |
| ApplySnowSoiling | number | No | A user-override for Solcast’s dynamic snow soiling, which is based on global snow cover and weather forecast data, and changes from hour to hour. If you specify this parameter in your API call (e.g. if snow clearing has just been performed), we will replace the Solcast dynamic hour to hour value with the single value you specify. E.g. if you specify a 0.7 snow soiling, your returned power will be reduced by 70%. |
| ApplyTrackerInactive | boolean | No | Indicating if trackers are inactive. If True, panels are assumed all facing up (i.e. zero rotation). Only has effect if your site has a tracking_type that is not “fixed”. |
| TerrainShading | boolean | No | If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects. |
| Format | Format | No | Response format. Default is HTML if not supplied. |

**Returns** `EstimatedActualsDataResponse`

| Field | Description |
| --- | --- |
| estimated_actuals | Estimated actual records returned for the requested location. |

## GetLiveHsuLosses

Get soiling loss estimated actuals using the HSU model for near real-time and past 7 days for the requested location.

`GET /data/live/soiling/hsu`

```csharp
var response = await client.Data.Live.Soiling.Hsu.GetAsync(request =>
{
    request.QueryParameters.Latitude = latitude;
    request.QueryParameters.Longitude = longitude;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| Latitude | number | Yes | The latitude of the location you request data for. Must be a decimal number between -90 and 90. |
| Longitude | number | Yes | The longitude of the location you request data for. Must be a decimal number between -180 and 180. |
| TimeZone | string | No | Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset. Default is utc. |
| Hours | integer | No | Time window of the response in hours from 1 to 336. Default is 24. |
| Period | Period | No | Length of the averaging period in ISO 8601 duration format. Default is PT30M. |
| DepoVelocPm10 | number | No | Deposition or settling velocity of PM10 particulates. [m/s]. Default is 0.004. |
| DepoVelocPm2.5 | number | No | Deposition or settling velocity of PM2.5 particulates. [m/s]. Default is 0.0009. |
| Tilt | number | No | The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90. |
| CleaningThreshold | number | No | Amount of daily rainfall required to clean the panels (mm). Default is 1.0. |
| InitialSoiling | number | No | Initial percentage of energy lost due to soiling at time zero in the rainfall series input. If not provided, Solcast will perform a ramp up series calculation to accurately determine this value. If provided must be >= 0 and < 0.3437. |
| ManualWashDates | string[] | No | Optional. Default is none. A list of ISO_8601 compliant dates or a repeating interval when manual cleaning of the panels occurred. A list of dates example: [2025-01-01,2025-01-05,2025-01-10]. A repeating interval example: R3/2025-01-01T00:00:00Z/P14D. Wash dates outside of the start and end of the request are discarded. |
| Format | Format | No | Response format |

**Returns** `EstimatedActualsDataResponse`

| Field | Description |
| --- | --- |
| estimated_actuals | Estimated actual records returned for the requested location. |

## GetLiveKimberLosses

Get soiling loss estimated actuals using the Kimber model for near real-time and past 7 days for the requested location.

`GET /data/live/soiling/kimber`

```csharp
var response = await client.Data.Live.Soiling.Kimber.GetAsync(request =>
{
    request.QueryParameters.Latitude = latitude;
    request.QueryParameters.Longitude = longitude;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| Latitude | number | Yes | The latitude of the location (EPSG:4326). Must be between -90 and 90. |
| Longitude | number | Yes | The longitude of the location (EPSG:4326). Must be between -180 and 180. |
| TimeZone | string | No | Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset. Default is utc. |
| Hours | integer | No | Time window of the response in hours from 1 to 336. Default is 24. |
| Period | Period | No | Length of the averaging period in ISO 8601 duration format. Default is PT30M. |
| CleaningThreshold | number | No | Amount of daily rainfall required to clean the panels (mm). Default is 6.0. Accepted range is 0 to 100. |
| SoilingLossRate | number | No | Fraction of energy lost due to one day of soiling. Default is 0.0015. |
| GracePeriod | integer | No | Number of days after a rainfall event when it's assumed the ground is damp (0 to 100). Default is 14 days. |
| MaxSoiling | number | No | Maximum fraction of energy lost due to soiling. Soiling will build up until this value (0 to 1). Default is 0.3. |
| InitialSoiling | number | No | Initial fraction of energy lost due to soiling at time zero in the rainfall series input (0 to 1). Default is 0. |
| ManualWashDates | string[] | No | Optional. Default is none. A list of ISO_8601 compliant dates or a repeating interval when manual cleaning of the panels occurred. A list of dates example: [2025-01-01,2025-01-05,2025-01-10]. A repeating interval example: R3/2025-01-01T00:00:00Z/P14D. Wash dates outside of the start and end of the request are discarded. |
| Format | Format | No | Response format. Default is HTML if not supplied. |

**Returns** `EstimatedActualsDataResponse`

| Field | Description |
| --- | --- |
| estimated_actuals | Estimated actual records returned for the requested location. |

## GetLiveRadiationAndWeather

Get irradiance and weather estimated actuals for near real-time and past 7 days for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data).

`GET /data/live/radiation_and_weather`

```csharp
var response = await client.Data.Live.RadiationAndWeather.GetAsync(request =>
{
    request.QueryParameters.Latitude = latitude;
    request.QueryParameters.Longitude = longitude;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| Hours | integer | No | The number of hours to return in the response. |
| Period | Period | No | Length of the averaging period in ISO 8601 duration format. Default is PT30M. |
| Tilt | number | No | The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90. |
| Azimuth | number | No | The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere. |
| ArrayType | ArrayType | No | The type of sun-tracking or geometry configuration of your site's modules. |
| OutputParameters | string[] | No | The output parameters to include in the response. Allowed values: air_temp, albedo, azimuth, clearsky_dhi, clearsky_dni, clearsky_ghi, clearsky_gti, cloud_opacity, dewpoint_temp, dhi, dni, ghi, gti, precipitable_water, precipitation_rate, relative_humidity, surface_pressure, snow_soiling_rooftop, snow_soiling_ground, wind_direction_100m, wind_direction_10m, wind_speed_100m, wind_speed_10m, zenith, snow_depth, snow_water_equivalent, snowfall_rate, wind_gust, cape, weather_type, min_air_temp, max_air_temp, pm10, pm2.5. |
| TerrainShading | boolean | No | If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects. |
| Latitude | number | Yes | The latitude of the location you request data for. Must be a decimal number between -90 and 90. |
| Longitude | number | Yes | The longitude of the location you request data for. Must be a decimal number between -180 and 180. |
| Format | Format | No | Response format. Default is HTML if not supplied. |

**Returns** `EstimatedActualsDataResponse`

| Field | Description |
| --- | --- |
| estimated_actuals | Estimated actual records returned for the requested location. |

## GetLiveRooftopPvPower

Get basic rooftop PV power estimated actuals for near real-time and past 7 days for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). The basic rooftop power simulation is only suitable for residential and smaller C&I rooftop sites, not for grid-scale sites.

`GET /data/live/rooftop_pv_power`

```csharp
var response = await client.Data.Live.RooftopPvPower.GetAsync(request =>
{
    request.QueryParameters.Latitude = latitude;
    request.QueryParameters.Longitude = longitude;
    request.QueryParameters.Capacity = capacity;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| Hours | integer | No | The number of hours to return in the response. |
| Period | Period | No | Length of the averaging period in ISO 8601 duration format. Default is PT30M. |
| Latitude | number | Yes | The latitude of the location you request data for. Must be a decimal number between -90 and 90. |
| Longitude | number | Yes | The longitude of the location you request data for. Must be a decimal number between -180 and 180. |
| Capacity | number | Yes | The capacity of the inverter (AC) or the modules (DC), whichever is greater, in kilowatts (kW). |
| Tilt | number | No | The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90. |
| Azimuth | number | No | The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere. |
| InstallDate | string | No | The date (yyyy-MM-dd) of installation of the PV system. We use this to estimate your loss_factor based on the ageing of your system. If you provide us with a loss_factor directly, we will ignore this date. |
| LossFactor | number | No | Default is 0.90 A factor to reduce your output forecast from the full capacity based on characteristics of the PV array or inverter. This is effectively the non-temperature loss effects on the nameplate rating of the PV system, including inefficiency and soiling. For a 1kW PV system anything that reduces 1000W/m2 solar radiation from producing 1000W of power output (assuming temperature is 25C). Valid values are between 0 and 1 (i.e. 0.6 equals 60%). If you specify 0.6 your returned power will be a maximum of 60% of AC capacity. |
| OutputParameters | string[] | No | The output parameters to include in the response. Allowed values: pv_power_rooftop. |
| TerrainShading | boolean | No | If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects. |
| Format | Format | No | Response format. Default is HTML if not supplied. |
| ApplySnowSoiling | number | No | A user-override for Solcast’s dynamic snow soiling, which is based on global snow cover and weather forecast data, and changes from hour to hour. If you specify this parameter in your API call (e.g. if snow clearing has just been performed), we will replace the Solcast dynamic hour to hour value with the single value you specify. E.g. if you specify a 0.7 snow soiling, your returned power will be reduced by 70%. |

**Returns** `EstimatedActualsDataResponse`

| Field | Description |
| --- | --- |
| estimated_actuals | Estimated actual records returned for the requested location. |
