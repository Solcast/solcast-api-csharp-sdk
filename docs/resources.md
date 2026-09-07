# Resources

For full details, see https://docs.solcast.com.au/

| Endpoint | Purpose |
| --- | --- |
| [CreatePremiumWindResource](#createpremiumwindresource) | Create a new premium wind power site. Required fields: name, latitude, longitude, capacity. Optional fields with defaults: rotor_diameter (100m), hub_height (80m), number_of_turbines (1), cut_in_wind_speed (3 m/s), cut_out_wind_speed (25 m/s), rated_wind_speed (12 m/s), allow_negative_power (false). |
| [CreatePvPowerResource](#createpvpowerresource) | Create Resource |
| [CreatePvSiteMeasurements](#createpvsitemeasurements) | Submit measurement data for a Premium PV Power site. Measurements are used for model training. Maximum 1000 measurements per request. Power units (AC) are measured in MW. Power is the actual measured power output by the site. If there are availability and curtailment constraints in place, those fields should also be included with the measurements. |
| [CreatePvSubUnitSiteMeasurements](#createpvsubunitsitemeasurements) | Submit sub-unit measurement data for a Premium PV Power site. A sub-unit represents a subsection of the site, such as an individual inverter or a subset of inverters. Each measurement must include a 'sub_unit' label. Maximum 1000 measurements per request. Power units (AC) are measured in MW. |
| [CreatePvUtilityMeasurement](#createpvutilitymeasurement) | Submit measurement data for an Advanced PV Power site. You can submit a single measurement, or an array of measurements. Maximum 1000 measurements per request. |
| [CreateWindSiteMeasurements](#createwindsitemeasurements) | Submit measurement data for a Premium Wind Power site. Measurements are used for model training. Maximum 1000 measurements per request. Power units (AC) are measured in MW. Power is the actual measured power output by the site. If there are availability and curtailment constraints in place, those fields should also be included with the measurements. |
| [CreateWindSubUnitSiteMeasurements](#createwindsubunitsitemeasurements) | Submit sub-unit measurement data for a Premium Wind Power site. A sub-unit represents a subsection of the site, such as an individual wind turbine or a subset of turbines. Each measurement must include a 'sub_unit' label. Maximum 1000 measurements per request. Power units (AC) are measured in MW. |
| [DeletePremiumWindResource](#deletepremiumwindresource) | Delete a premium wind power site. |
| [DeletePvPowerResource](#deletepvpowerresource) | Remove Resource |
| [DeletePvSiteMeasurements](#deletepvsitemeasurements) | Delete site measurements |
| [DeletePvSubUnitSiteMeasurements](#deletepvsubunitsitemeasurements) | Delete sub-unit site measurements |
| [DeleteWindSiteMeasurements](#deletewindsitemeasurements) | Delete site measurements |
| [DeleteWindSubUnitSiteMeasurements](#deletewindsubunitsitemeasurements) | Delete sub-unit site measurements |
| [GetPremiumWindResource](#getpremiumwindresource) | Retrieve details of a specific premium wind power site. |
| [GetPvPowerResource](#getpvpowerresource) | Get Resource |
| [GetPvSiteMeasurements](#getpvsitemeasurements) | Retrieve historical measurement data for a Premium PV Power site. Supports pagination via skip/take and date filtering via start/end. |
| [GetPvSubUnitSiteMeasurements](#getpvsubunitsitemeasurements) | Retrieve historical sub-unit measurement data for a Premium PV Power site. A sub-unit represents a subsection of the site, such as an individual inverter or a subset of inverters. Supports pagination via skip/take, date filtering via start/end, and an optional sub_unit filter. |
| [GetWindSiteMeasurements](#getwindsitemeasurements) | Retrieve historical measurement data for a Premium Wind Power site. Supports pagination via skip/take and date filtering via start/end. |
| [GetWindSubUnitSiteMeasurements](#getwindsubunitsitemeasurements) | Retrieve historical sub-unit measurement data for a Premium Wind Power site. A sub-unit represents a subsection of the site, such as an individual wind turbine or a subset of turbines. Supports pagination via skip/take, date filtering via start/end, and an optional sub_unit filter. |
| [ListPremiumWindResources](#listpremiumwindresources) | Lists all premium wind power sites accessible to the authenticated user. |
| [ListPvPowerResources](#listpvpowerresources) | Lists all PV power sites accessible to the authenticated user. Supports pagination (skip/take), entitlement filtering (advanced/premium), and date range filtering (start/end). |
| [PatchPremiumWindResource](#patchpremiumwindresource) | Partially update a premium wind power site. Only fields included in the body are updated. All body fields: resource_id (required), name, latitude, longitude, capacity, rotor_diameter (20-200), rated_power (0.1-15000), hub_height (10-200), number_of_turbines (1-500), cut_in_wind_speed (0-10), cut_out_wind_speed (15-35), rated_wind_speed (8-20), allow_negative_power (true or false). |
| [PatchPvPowerResource](#patchpvpowerresource) | Patch Resource |
| [UpdatePremiumWindResource](#updatepremiumwindresource) | Full replacement update of a premium wind power site. All fields: resource_id (required), name, latitude (-90 to 90), longitude (-180 to 180), capacity (>0 MW), rotor_diameter (20-200m), rated_power (0.1-15000 MW), hub_height (10-200m), number_of_turbines (1-500), cut_in_wind_speed (0-10 m/s), cut_out_wind_speed (15-35 m/s), rated_wind_speed (8-20 m/s), allow_negative_power (true or false). |
| [UpdatePvPowerResource](#updatepvpowerresource) | Update Resource |

## CreatePremiumWindResource

Create a new premium wind power site. Required fields: name, latitude, longitude, capacity. Optional fields with defaults: rotor_diameter (100m), hub_height (80m), number_of_turbines (1), cut_in_wind_speed (3 m/s), cut_out_wind_speed (25 m/s), rated_wind_speed (12 m/s), allow_negative_power (false).

`POST /resources/wind_power_site`

```csharp
var response = await client.Resources.WindPowerSite.PostAsync();
```

**Returns** `PremiumWindPowerResource`

| Field | Description |
| --- | --- |
| resource_id | The unique identifier of the resource. |
| name | The name of the resource. |
| latitude | The latitude of the resource. Must be a decimal number between -90 and 90. |
| longitude | The longitude of the resource. Must be between -180 and 180. |
| capacity | The site capacity (rated power × number of turbines) in MW. |
| rotor_diameter | Average diameter of turbine rotor in metres. Must be between 20 and 200. |
| rated_power | Average turbine rated power in MW. Must be between 0.1 and 15000. |
| hub_height | Average height of turbine hub in metres. Must be between 10 and 200. Defaults to 80. |
| number_of_turbines | Total number of turbines at site. Must be between 1 and 500. Defaults to 1. |
| cut_in_wind_speed | Minimum wind speed for generation in m/s. Must be between 0 and 10. Defaults to 3. |
| cut_out_wind_speed | Maximum wind speed before shutdown in m/s. Must be between 15 and 35. Defaults to 25. |
| rated_wind_speed | Wind speed at rated power in m/s. Must be between 8 and 20. Defaults to 12. |
| allow_negative_power | Whether forecasts for this site may contain negative power values. Defaults to false. This setting is used when predictions are produced; the API does not modify forecast values. |
| grid_export_limit | Optional maximum power export allowed by the site's grid connection, in MW. Omit this field when no limit is configured. Values >= 0 configure a limit. Value of -1 is treated as no configured limit. A configured limit will be in effect the next time predictions are updated. |
| forecast_production_enabled | Indicates whether ML forecast production is enabled for this site. |

## CreatePvPowerResource

Create Resource

`POST /resources/pv_power_site`

```csharp
var response = await client.Resources.PvPowerSite.PostAsync();
```

**Returns** `PvPowerResource`

| Field | Description |
| --- | --- |
| resource_id | The unique identifier of the resource. |
| name | The name of the resource. |
| latitude | The latitude of the resource. Must be a decimal number between -90 and 90. |
| longitude | The longitude of the resource. Must be a decimal number between -180 and 180. |
| capacity | Total inverter (nameplate) capacity in MW. This is the highest potential output of the system before any Site Export Limit is applied. It is used to model the conversion of DC power to AC by your inverters. |
| capacity_dc | Total module capacity in MW. Usually slightly higher than the AC capacity. It is used to model the generation of DC power by your modules. |
| azimuth | The angle from true north the modules are facing. North is 0, South is &#177;180, Eastward facing is negative values. Westward facing is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| tilt | The off-horizontal tilt angle of modules for a fixed-tilt site. |
| tracking_type | The type of sun-tracking or geometry configuration of your site's modules. It is used to calculate the incident irradiance for your modules. |
| install_date | The date when your site was installed. It is used to derate your module (DC) production gradually with age, at a rate dependent on your Module Type. |
| grid_export_limit | Optional maximum power export allowed by the site's grid connection, in MW. Omit this field when no limit is configured. Values >= 0 configure a limit. Value of -1 is treated as no configured limit. Only impacts your AC power if the grid export limit is set lower than the AC capacity. A configured limit will be in effect the next time predictions are updated. |
| module_type | The type of material or technology used in your site's PV modules. It is used to estimate your module temperature derating coefficient (unless you specify your own coefficient), and used to estimate your module age derating. |
| ground_coverage_ratio | The proportion of the site's ground area covered by modules. It is used to calculate the incident irradiance for your modules. Must be at least 0.01 and at most 1. |
| derating_temp_module | The factor by which your site’s module (DC) production varies with differences in temperature from standard operating conditions ( 25 oC ). Can be found on module spec sheets and its often referred as: temperature coefficient of power or pmp. |
| derating_age_system | The factor by which the whole system will be derated per year since Install Date. It is used to calculate time dependent system loss. |
| derating_other_system | The total system losses as a fraction of the total energy output. This excludes shading, soiling, snow, inverter, temperature and age. Includes losses such as wiring. |
| inverter_peak_efficiency | The peak efficiency value in your inverter efficiency curve. It is used to scale the conversion efficiency of DC to AC, as a function of the inverter load. Must be at least 0.01 and at most 1. |
| tracker_axis_azimuth | The off north-south azimuth angle for a horizontal single axis tracking site. Most commonly this will be close to zero. North is 0, South is &#177;180, Eastward facing is negative values. Westward facing is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| tracker_max_rotation_angle | The maximum off-horizontal angle for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| tracker_back_tracking | Whether the trackers backtrack at low solar elevation angles, for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| tracker_smart_tracking | Whether the trackers move to horizontal during cloudy periods with zero DNI, for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| terrain_slope | The average terrain slope in degrees from horizontal of your site. A site with no terrain slope has a value of zero. |
| terrain_azimuth | The average terrain slope downhill direction. North is 0, South is &#177;180, Eastward is negative values. Westward is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| dust_soiling_average | The average proportion of module production lost due to dust soiling. The value entered should reflect the impact of cleaning activity at your site. It is used to calculate the module (DC) production. |
| bifacial_system | Bifacial systems have modules that produce solar power not only from the front, but also the rear side of the panel. Used to create additional parameters that will allow us to model your production more accurately. |
| site_ground_albedo | The proportion of the incident downward irradiance reflected by the ground surface in the area underneath the PV modules. Used to calculate local ground-reflected irradiance. Particularly important for bifacial systems. |
| bifaciality_factor | For bifacial modules, the module rear efficiency as a proportion of the front efficiency subject to the same irradiance. Used to calculate the module rear production for bifacial systems. |
| pvrow_height | The height of the module rows, in metres, measured from the ground to the row center or axis. Used to calculate the module rear incident irradiance and production for bifacial systems. |
| pvrow_width | The width of the module rows, in metres. This is the cross-section width of the entire PV row. Used to calculate the module rear incident irradiance and production for bifacial systems. |
| clearsky_zenith_coefficients | Coefficients used to model clear-sky solar zenith irradiance for the resource. |
| cloudy_zenith_coefficients | Coefficients used to model cloudy-sky solar zenith irradiance for the resource. |
| forecast_production_enabled | Indicates whether ML forecast production is enabled for this site (premium only). |

## CreatePvSiteMeasurements

Submit measurement data for a Premium PV Power site. Measurements are used for model training. Maximum 1000 measurements per request. Power units (AC) are measured in MW. Power is the actual measured power output by the site. If there are availability and curtailment constraints in place, those fields should also be included with the measurements.

`POST /resources/pv_power_site_measurements`

```csharp
var response = await client.Resources.PvPowerSiteMeasurements.PostAsync();
```

**Returns** `CreateSiteMeasurementsResponse`

## CreatePvSubUnitSiteMeasurements

Submit sub-unit measurement data for a Premium PV Power site. A sub-unit represents a subsection of the site, such as an individual inverter or a subset of inverters. Each measurement must include a 'sub_unit' label. Maximum 1000 measurements per request. Power units (AC) are measured in MW.

`POST /resources/pv_power_site_measurements/sub_units`

```csharp
var response = await client.Resources.PvPowerSiteMeasurements.SubUnits.PostAsync();
```

**Returns** `CreateSiteMeasurementsResponse`

## CreatePvUtilityMeasurement

Submit measurement data for an Advanced PV Power site. You can submit a single measurement, or an array of measurements. Maximum 1000 measurements per request.

`POST /utility_scale_sites/{resource_id}/measurements`

```csharp
var response = await client.UtilityScaleSites["{resource_id}"].Measurements.PostAsync();
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the Advanced PV Power resource. |

**Returns** `CreatePvUtilityMeasurementResponse`

| Field | Description |
| --- | --- |
| site_resource_id | The unique identifier of the site resource. |
| measurement | The submitted measurement record, when a single measurement was provided. |
| measurements | The submitted measurement records, when an array of measurements was provided. |
| response_status | Error details returned when the request fails; omitted on success. |

## CreateWindSiteMeasurements

Submit measurement data for a Premium Wind Power site. Measurements are used for model training. Maximum 1000 measurements per request. Power units (AC) are measured in MW. Power is the actual measured power output by the site. If there are availability and curtailment constraints in place, those fields should also be included with the measurements.

`POST /resources/wind_power_site_measurements`

```csharp
var response = await client.Resources.WindPowerSiteMeasurements.PostAsync();
```

**Returns** `CreateSiteMeasurementsResponse`

## CreateWindSubUnitSiteMeasurements

Submit sub-unit measurement data for a Premium Wind Power site. A sub-unit represents a subsection of the site, such as an individual wind turbine or a subset of turbines. Each measurement must include a 'sub_unit' label. Maximum 1000 measurements per request. Power units (AC) are measured in MW.

`POST /resources/wind_power_site_measurements/sub_units`

```csharp
var response = await client.Resources.WindPowerSiteMeasurements.SubUnits.PostAsync();
```

**Returns** `CreateSiteMeasurementsResponse`

## DeletePremiumWindResource

Delete a premium wind power site.

`DELETE /resources/wind_power_site`

```csharp
var response = await client.Resources.WindPowerSite.DeleteAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the resource. |

## DeletePvPowerResource

Remove Resource

`DELETE /resources/pv_power_site`

```csharp
var response = await client.Resources.PvPowerSite.DeleteAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the resource. |

## DeletePvSiteMeasurements

Delete site measurements

`DELETE /resources/pv_power_site_measurements`

```csharp
var response = await client.Resources.PvPowerSiteMeasurements.DeleteAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
    request.QueryParameters.Start = start;
    request.QueryParameters.End = end;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the Premium PV Power resource. |
| Start | string | Yes | Delete lower bound timestamp (ISO-8601). |
| End | string | Yes | Delete upper bound timestamp (ISO-8601). |

**Returns** `DeleteSiteMeasurementsResponse`

## DeletePvSubUnitSiteMeasurements

Delete sub-unit site measurements

`DELETE /resources/pv_power_site_measurements/sub_units`

```csharp
var response = await client.Resources.PvPowerSiteMeasurements.SubUnits.DeleteAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
    request.QueryParameters.Start = start;
    request.QueryParameters.End = end;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the Premium PV Power resource. |
| SubUnit | string | No | Optional filter: delete only measurements for this sub_unit. |
| Start | string | Yes | Delete lower bound timestamp (ISO-8601). |
| End | string | Yes | Delete upper bound timestamp (ISO-8601). |

**Returns** `DeleteSiteMeasurementsResponse`

## DeleteWindSiteMeasurements

Delete site measurements

`DELETE /resources/wind_power_site_measurements`

```csharp
var response = await client.Resources.WindPowerSiteMeasurements.DeleteAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
    request.QueryParameters.Start = start;
    request.QueryParameters.End = end;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the Premium Wind Power resource. |
| Start | string | Yes | Delete lower bound timestamp (ISO-8601). |
| End | string | Yes | Delete upper bound timestamp (ISO-8601). |

**Returns** `DeleteSiteMeasurementsResponse`

## DeleteWindSubUnitSiteMeasurements

Delete sub-unit site measurements

`DELETE /resources/wind_power_site_measurements/sub_units`

```csharp
var response = await client.Resources.WindPowerSiteMeasurements.SubUnits.DeleteAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
    request.QueryParameters.Start = start;
    request.QueryParameters.End = end;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the Premium Wind Power resource. |
| SubUnit | string | No | Optional filter: delete only measurements for this sub_unit. |
| Start | string | Yes | Delete lower bound timestamp (ISO-8601). |
| End | string | Yes | Delete upper bound timestamp (ISO-8601). |

**Returns** `DeleteSiteMeasurementsResponse`

## GetPremiumWindResource

Retrieve details of a specific premium wind power site.

`GET /resources/wind_power_site`

```csharp
var response = await client.Resources.WindPowerSite.GetAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the resource. |
| Format | Format | No | Response format. Default is HTML if not supplied. |

**Returns** `PremiumWindPowerResource`

| Field | Description |
| --- | --- |
| resource_id | The unique identifier of the resource. |
| name | The name of the resource. |
| latitude | The latitude of the resource. Must be a decimal number between -90 and 90. |
| longitude | The longitude of the resource. Must be between -180 and 180. |
| capacity | The site capacity (rated power × number of turbines) in MW. |
| rotor_diameter | Average diameter of turbine rotor in metres. Must be between 20 and 200. |
| rated_power | Average turbine rated power in MW. Must be between 0.1 and 15000. |
| hub_height | Average height of turbine hub in metres. Must be between 10 and 200. Defaults to 80. |
| number_of_turbines | Total number of turbines at site. Must be between 1 and 500. Defaults to 1. |
| cut_in_wind_speed | Minimum wind speed for generation in m/s. Must be between 0 and 10. Defaults to 3. |
| cut_out_wind_speed | Maximum wind speed before shutdown in m/s. Must be between 15 and 35. Defaults to 25. |
| rated_wind_speed | Wind speed at rated power in m/s. Must be between 8 and 20. Defaults to 12. |
| allow_negative_power | Whether forecasts for this site may contain negative power values. Defaults to false. This setting is used when predictions are produced; the API does not modify forecast values. |
| grid_export_limit | Optional maximum power export allowed by the site's grid connection, in MW. Omit this field when no limit is configured. Values >= 0 configure a limit. Value of -1 is treated as no configured limit. A configured limit will be in effect the next time predictions are updated. |
| forecast_production_enabled | Indicates whether ML forecast production is enabled for this site. |

## GetPvPowerResource

Get Resource

`GET /resources/pv_power_site`

```csharp
var response = await client.Resources.PvPowerSite.GetAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the resource. |
| Format | Format | No | Response format. Default is HTML if not supplied. |

**Returns** `PvPowerResource`

| Field | Description |
| --- | --- |
| resource_id | The unique identifier of the resource. |
| name | The name of the resource. |
| latitude | The latitude of the resource. Must be a decimal number between -90 and 90. |
| longitude | The longitude of the resource. Must be a decimal number between -180 and 180. |
| capacity | Total inverter (nameplate) capacity in MW. This is the highest potential output of the system before any Site Export Limit is applied. It is used to model the conversion of DC power to AC by your inverters. |
| capacity_dc | Total module capacity in MW. Usually slightly higher than the AC capacity. It is used to model the generation of DC power by your modules. |
| azimuth | The angle from true north the modules are facing. North is 0, South is &#177;180, Eastward facing is negative values. Westward facing is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| tilt | The off-horizontal tilt angle of modules for a fixed-tilt site. |
| tracking_type | The type of sun-tracking or geometry configuration of your site's modules. It is used to calculate the incident irradiance for your modules. |
| install_date | The date when your site was installed. It is used to derate your module (DC) production gradually with age, at a rate dependent on your Module Type. |
| grid_export_limit | Optional maximum power export allowed by the site's grid connection, in MW. Omit this field when no limit is configured. Values >= 0 configure a limit. Value of -1 is treated as no configured limit. Only impacts your AC power if the grid export limit is set lower than the AC capacity. A configured limit will be in effect the next time predictions are updated. |
| module_type | The type of material or technology used in your site's PV modules. It is used to estimate your module temperature derating coefficient (unless you specify your own coefficient), and used to estimate your module age derating. |
| ground_coverage_ratio | The proportion of the site's ground area covered by modules. It is used to calculate the incident irradiance for your modules. Must be at least 0.01 and at most 1. |
| derating_temp_module | The factor by which your site’s module (DC) production varies with differences in temperature from standard operating conditions ( 25 oC ). Can be found on module spec sheets and its often referred as: temperature coefficient of power or pmp. |
| derating_age_system | The factor by which the whole system will be derated per year since Install Date. It is used to calculate time dependent system loss. |
| derating_other_system | The total system losses as a fraction of the total energy output. This excludes shading, soiling, snow, inverter, temperature and age. Includes losses such as wiring. |
| inverter_peak_efficiency | The peak efficiency value in your inverter efficiency curve. It is used to scale the conversion efficiency of DC to AC, as a function of the inverter load. Must be at least 0.01 and at most 1. |
| tracker_axis_azimuth | The off north-south azimuth angle for a horizontal single axis tracking site. Most commonly this will be close to zero. North is 0, South is &#177;180, Eastward facing is negative values. Westward facing is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| tracker_max_rotation_angle | The maximum off-horizontal angle for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| tracker_back_tracking | Whether the trackers backtrack at low solar elevation angles, for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| tracker_smart_tracking | Whether the trackers move to horizontal during cloudy periods with zero DNI, for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| terrain_slope | The average terrain slope in degrees from horizontal of your site. A site with no terrain slope has a value of zero. |
| terrain_azimuth | The average terrain slope downhill direction. North is 0, South is &#177;180, Eastward is negative values. Westward is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| dust_soiling_average | The average proportion of module production lost due to dust soiling. The value entered should reflect the impact of cleaning activity at your site. It is used to calculate the module (DC) production. |
| bifacial_system | Bifacial systems have modules that produce solar power not only from the front, but also the rear side of the panel. Used to create additional parameters that will allow us to model your production more accurately. |
| site_ground_albedo | The proportion of the incident downward irradiance reflected by the ground surface in the area underneath the PV modules. Used to calculate local ground-reflected irradiance. Particularly important for bifacial systems. |
| bifaciality_factor | For bifacial modules, the module rear efficiency as a proportion of the front efficiency subject to the same irradiance. Used to calculate the module rear production for bifacial systems. |
| pvrow_height | The height of the module rows, in metres, measured from the ground to the row center or axis. Used to calculate the module rear incident irradiance and production for bifacial systems. |
| pvrow_width | The width of the module rows, in metres. This is the cross-section width of the entire PV row. Used to calculate the module rear incident irradiance and production for bifacial systems. |
| clearsky_zenith_coefficients | Coefficients used to model clear-sky solar zenith irradiance for the resource. |
| cloudy_zenith_coefficients | Coefficients used to model cloudy-sky solar zenith irradiance for the resource. |
| forecast_production_enabled | Indicates whether ML forecast production is enabled for this site (premium only). |

## GetPvSiteMeasurements

Retrieve historical measurement data for a Premium PV Power site. Supports pagination via skip/take and date filtering via start/end.

`GET /resources/pv_power_site_measurements`

```csharp
var response = await client.Resources.PvPowerSiteMeasurements.GetAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the Premium PV Power resource. |
| Start | string | No | Filter: period_end >= start (ISO-8601). |
| End | string | No | Filter: period_end <= end (ISO-8601). |
| Format | Format | No | Response format. Default is HTML if not supplied. |
| Skip | integer | No | Number of records to skip before returning results. |
| Take | integer | No | Maximum number of records to return. |

**Returns** `PvSiteMeasurementsResponse`

## GetPvSubUnitSiteMeasurements

Retrieve historical sub-unit measurement data for a Premium PV Power site. A sub-unit represents a subsection of the site, such as an individual inverter or a subset of inverters. Supports pagination via skip/take, date filtering via start/end, and an optional sub_unit filter.

`GET /resources/pv_power_site_measurements/sub_units`

```csharp
var response = await client.Resources.PvPowerSiteMeasurements.SubUnits.GetAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the Premium PV Power resource. |
| SubUnit | string | No | Optional filter: return only measurements for this sub_unit. |
| Start | string | No | Filter: period_end >= start (ISO-8601). |
| End | string | No | Filter: period_end <= end (ISO-8601). |
| Format | Format | No | Response format. Default is HTML if not supplied. |
| Skip | integer | No | Number of records to skip before returning results. |
| Take | integer | No | Maximum number of records to return. |

**Returns** `PvSubUnitSiteMeasurementsResponse`

## GetWindSiteMeasurements

Retrieve historical measurement data for a Premium Wind Power site. Supports pagination via skip/take and date filtering via start/end.

`GET /resources/wind_power_site_measurements`

```csharp
var response = await client.Resources.WindPowerSiteMeasurements.GetAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the Premium Wind Power resource. |
| Start | string | No | Filter: period_end >= start (ISO-8601). |
| End | string | No | Filter: period_end <= end (ISO-8601). |
| Format | Format | No | Response format. Default is HTML if not supplied. |
| Skip | integer | No | Number of records to skip before returning results. |
| Take | integer | No | Maximum number of records to return. |

**Returns** `WindSiteMeasurementsResponse`

## GetWindSubUnitSiteMeasurements

Retrieve historical sub-unit measurement data for a Premium Wind Power site. A sub-unit represents a subsection of the site, such as an individual wind turbine or a subset of turbines. Supports pagination via skip/take, date filtering via start/end, and an optional sub_unit filter.

`GET /resources/wind_power_site_measurements/sub_units`

```csharp
var response = await client.Resources.WindPowerSiteMeasurements.SubUnits.GetAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The unique identifier of the Premium Wind Power resource. |
| SubUnit | string | No | Optional filter: return only measurements for this sub_unit. |
| Start | string | No | Filter: period_end >= start (ISO-8601). |
| End | string | No | Filter: period_end <= end (ISO-8601). |
| Format | Format | No | Response format. Default is HTML if not supplied. |
| Skip | integer | No | Number of records to skip before returning results. |
| Take | integer | No | Maximum number of records to return. |

**Returns** `WindSubUnitSiteMeasurementsResponse`

## ListPremiumWindResources

Lists all premium wind power sites accessible to the authenticated user.

`GET /resources/wind_power_sites`

```csharp
var response = await client.Resources.WindPowerSites.GetAsync();
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ShowAll | boolean | No | When true, returns all sites in the system. Requires admin privileges. |
| Query | string | No | Search query for site name or resource ID. |
| Start | string | No | Filter to sites created on or after this date (ISO 8601 format). |
| End | string | No | Filter to sites created before this date (ISO 8601 format). |
| ForecastProduction | boolean | No | Filter to sites where ML forecast production is enabled. |
| Format | Format | No | Response format. Default is HTML if not supplied. |
| Skip | integer | No | Number of records to skip before returning results. |
| Take | integer | No | Maximum number of records to return. |

**Returns** `QueryResponse_PremiumWindPowerResource`

## ListPvPowerResources

Lists all PV power sites accessible to the authenticated user. Supports pagination (skip/take), entitlement filtering (advanced/premium), and date range filtering (start/end).

`GET /resources/pv_power_sites`

```csharp
var response = await client.Resources.PvPowerSites.GetAsync();
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| Entitlement | Entitlement | No | Optional filter for site entitlement. |
| ShowAll | boolean | No | When true, returns all sites in the system. Requires admin privileges. |
| Start | string | No | Filter to sites created on or after this date (ISO 8601 format). |
| End | string | No | Filter to sites created before this date (ISO 8601 format). |
| Query | string | No | Search query for site name or resource ID. |
| ForecastProduction | boolean | No | Filter to sites where ML forecast production is enabled (premium only). |
| Format | Format | No | Response format. Default is HTML if not supplied. |
| Skip | integer | No | Number of records to skip before returning results. |
| Take | integer | No | Maximum number of records to return. |

**Returns** `QueryResponse_PvPowerResource`

## PatchPremiumWindResource

Partially update a premium wind power site. Only fields included in the body are updated. All body fields: resource_id (required), name, latitude, longitude, capacity, rotor_diameter (20-200), rated_power (0.1-15000), hub_height (10-200), number_of_turbines (1-500), cut_in_wind_speed (0-10), cut_out_wind_speed (15-35), rated_wind_speed (8-20), allow_negative_power (true or false).

`PATCH /resources/wind_power_site`

```csharp
var response = await client.Resources.WindPowerSite.PatchAsync();
```

**Returns** `PremiumWindPowerResource`

| Field | Description |
| --- | --- |
| resource_id | The unique identifier of the resource. |
| name | The name of the resource. |
| latitude | The latitude of the resource. Must be a decimal number between -90 and 90. |
| longitude | The longitude of the resource. Must be between -180 and 180. |
| capacity | The site capacity (rated power × number of turbines) in MW. |
| rotor_diameter | Average diameter of turbine rotor in metres. Must be between 20 and 200. |
| rated_power | Average turbine rated power in MW. Must be between 0.1 and 15000. |
| hub_height | Average height of turbine hub in metres. Must be between 10 and 200. Defaults to 80. |
| number_of_turbines | Total number of turbines at site. Must be between 1 and 500. Defaults to 1. |
| cut_in_wind_speed | Minimum wind speed for generation in m/s. Must be between 0 and 10. Defaults to 3. |
| cut_out_wind_speed | Maximum wind speed before shutdown in m/s. Must be between 15 and 35. Defaults to 25. |
| rated_wind_speed | Wind speed at rated power in m/s. Must be between 8 and 20. Defaults to 12. |
| allow_negative_power | Whether forecasts for this site may contain negative power values. Defaults to false. This setting is used when predictions are produced; the API does not modify forecast values. |
| grid_export_limit | Optional maximum power export allowed by the site's grid connection, in MW. Omit this field when no limit is configured. Values >= 0 configure a limit. Value of -1 is treated as no configured limit. A configured limit will be in effect the next time predictions are updated. |
| forecast_production_enabled | Indicates whether ML forecast production is enabled for this site. |

## PatchPvPowerResource

Patch Resource

`PATCH /resources/pv_power_site`

```csharp
var response = await client.Resources.PvPowerSite.PatchAsync();
```

**Returns** `PvPowerResource`

| Field | Description |
| --- | --- |
| resource_id | The unique identifier of the resource. |
| name | The name of the resource. |
| latitude | The latitude of the resource. Must be a decimal number between -90 and 90. |
| longitude | The longitude of the resource. Must be a decimal number between -180 and 180. |
| capacity | Total inverter (nameplate) capacity in MW. This is the highest potential output of the system before any Site Export Limit is applied. It is used to model the conversion of DC power to AC by your inverters. |
| capacity_dc | Total module capacity in MW. Usually slightly higher than the AC capacity. It is used to model the generation of DC power by your modules. |
| azimuth | The angle from true north the modules are facing. North is 0, South is &#177;180, Eastward facing is negative values. Westward facing is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| tilt | The off-horizontal tilt angle of modules for a fixed-tilt site. |
| tracking_type | The type of sun-tracking or geometry configuration of your site's modules. It is used to calculate the incident irradiance for your modules. |
| install_date | The date when your site was installed. It is used to derate your module (DC) production gradually with age, at a rate dependent on your Module Type. |
| grid_export_limit | Optional maximum power export allowed by the site's grid connection, in MW. Omit this field when no limit is configured. Values >= 0 configure a limit. Value of -1 is treated as no configured limit. Only impacts your AC power if the grid export limit is set lower than the AC capacity. A configured limit will be in effect the next time predictions are updated. |
| module_type | The type of material or technology used in your site's PV modules. It is used to estimate your module temperature derating coefficient (unless you specify your own coefficient), and used to estimate your module age derating. |
| ground_coverage_ratio | The proportion of the site's ground area covered by modules. It is used to calculate the incident irradiance for your modules. Must be at least 0.01 and at most 1. |
| derating_temp_module | The factor by which your site’s module (DC) production varies with differences in temperature from standard operating conditions ( 25 oC ). Can be found on module spec sheets and its often referred as: temperature coefficient of power or pmp. |
| derating_age_system | The factor by which the whole system will be derated per year since Install Date. It is used to calculate time dependent system loss. |
| derating_other_system | The total system losses as a fraction of the total energy output. This excludes shading, soiling, snow, inverter, temperature and age. Includes losses such as wiring. |
| inverter_peak_efficiency | The peak efficiency value in your inverter efficiency curve. It is used to scale the conversion efficiency of DC to AC, as a function of the inverter load. Must be at least 0.01 and at most 1. |
| tracker_axis_azimuth | The off north-south azimuth angle for a horizontal single axis tracking site. Most commonly this will be close to zero. North is 0, South is &#177;180, Eastward facing is negative values. Westward facing is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| tracker_max_rotation_angle | The maximum off-horizontal angle for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| tracker_back_tracking | Whether the trackers backtrack at low solar elevation angles, for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| tracker_smart_tracking | Whether the trackers move to horizontal during cloudy periods with zero DNI, for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| terrain_slope | The average terrain slope in degrees from horizontal of your site. A site with no terrain slope has a value of zero. |
| terrain_azimuth | The average terrain slope downhill direction. North is 0, South is &#177;180, Eastward is negative values. Westward is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| dust_soiling_average | The average proportion of module production lost due to dust soiling. The value entered should reflect the impact of cleaning activity at your site. It is used to calculate the module (DC) production. |
| bifacial_system | Bifacial systems have modules that produce solar power not only from the front, but also the rear side of the panel. Used to create additional parameters that will allow us to model your production more accurately. |
| site_ground_albedo | The proportion of the incident downward irradiance reflected by the ground surface in the area underneath the PV modules. Used to calculate local ground-reflected irradiance. Particularly important for bifacial systems. |
| bifaciality_factor | For bifacial modules, the module rear efficiency as a proportion of the front efficiency subject to the same irradiance. Used to calculate the module rear production for bifacial systems. |
| pvrow_height | The height of the module rows, in metres, measured from the ground to the row center or axis. Used to calculate the module rear incident irradiance and production for bifacial systems. |
| pvrow_width | The width of the module rows, in metres. This is the cross-section width of the entire PV row. Used to calculate the module rear incident irradiance and production for bifacial systems. |
| clearsky_zenith_coefficients | Coefficients used to model clear-sky solar zenith irradiance for the resource. |
| cloudy_zenith_coefficients | Coefficients used to model cloudy-sky solar zenith irradiance for the resource. |
| forecast_production_enabled | Indicates whether ML forecast production is enabled for this site (premium only). |

## UpdatePremiumWindResource

Full replacement update of a premium wind power site. All fields: resource_id (required), name, latitude (-90 to 90), longitude (-180 to 180), capacity (>0 MW), rotor_diameter (20-200m), rated_power (0.1-15000 MW), hub_height (10-200m), number_of_turbines (1-500), cut_in_wind_speed (0-10 m/s), cut_out_wind_speed (15-35 m/s), rated_wind_speed (8-20 m/s), allow_negative_power (true or false).

`PUT /resources/wind_power_site`

```csharp
var response = await client.Resources.WindPowerSite.PutAsync();
```

**Returns** `PremiumWindPowerResource`

| Field | Description |
| --- | --- |
| resource_id | The unique identifier of the resource. |
| name | The name of the resource. |
| latitude | The latitude of the resource. Must be a decimal number between -90 and 90. |
| longitude | The longitude of the resource. Must be between -180 and 180. |
| capacity | The site capacity (rated power × number of turbines) in MW. |
| rotor_diameter | Average diameter of turbine rotor in metres. Must be between 20 and 200. |
| rated_power | Average turbine rated power in MW. Must be between 0.1 and 15000. |
| hub_height | Average height of turbine hub in metres. Must be between 10 and 200. Defaults to 80. |
| number_of_turbines | Total number of turbines at site. Must be between 1 and 500. Defaults to 1. |
| cut_in_wind_speed | Minimum wind speed for generation in m/s. Must be between 0 and 10. Defaults to 3. |
| cut_out_wind_speed | Maximum wind speed before shutdown in m/s. Must be between 15 and 35. Defaults to 25. |
| rated_wind_speed | Wind speed at rated power in m/s. Must be between 8 and 20. Defaults to 12. |
| allow_negative_power | Whether forecasts for this site may contain negative power values. Defaults to false. This setting is used when predictions are produced; the API does not modify forecast values. |
| grid_export_limit | Optional maximum power export allowed by the site's grid connection, in MW. Omit this field when no limit is configured. Values >= 0 configure a limit. Value of -1 is treated as no configured limit. A configured limit will be in effect the next time predictions are updated. |
| forecast_production_enabled | Indicates whether ML forecast production is enabled for this site. |

## UpdatePvPowerResource

Update Resource

`PUT /resources/pv_power_site`

```csharp
var response = await client.Resources.PvPowerSite.PutAsync();
```

**Returns** `PvPowerResource`

| Field | Description |
| --- | --- |
| resource_id | The unique identifier of the resource. |
| name | The name of the resource. |
| latitude | The latitude of the resource. Must be a decimal number between -90 and 90. |
| longitude | The longitude of the resource. Must be a decimal number between -180 and 180. |
| capacity | Total inverter (nameplate) capacity in MW. This is the highest potential output of the system before any Site Export Limit is applied. It is used to model the conversion of DC power to AC by your inverters. |
| capacity_dc | Total module capacity in MW. Usually slightly higher than the AC capacity. It is used to model the generation of DC power by your modules. |
| azimuth | The angle from true north the modules are facing. North is 0, South is &#177;180, Eastward facing is negative values. Westward facing is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| tilt | The off-horizontal tilt angle of modules for a fixed-tilt site. |
| tracking_type | The type of sun-tracking or geometry configuration of your site's modules. It is used to calculate the incident irradiance for your modules. |
| install_date | The date when your site was installed. It is used to derate your module (DC) production gradually with age, at a rate dependent on your Module Type. |
| grid_export_limit | Optional maximum power export allowed by the site's grid connection, in MW. Omit this field when no limit is configured. Values >= 0 configure a limit. Value of -1 is treated as no configured limit. Only impacts your AC power if the grid export limit is set lower than the AC capacity. A configured limit will be in effect the next time predictions are updated. |
| module_type | The type of material or technology used in your site's PV modules. It is used to estimate your module temperature derating coefficient (unless you specify your own coefficient), and used to estimate your module age derating. |
| ground_coverage_ratio | The proportion of the site's ground area covered by modules. It is used to calculate the incident irradiance for your modules. Must be at least 0.01 and at most 1. |
| derating_temp_module | The factor by which your site’s module (DC) production varies with differences in temperature from standard operating conditions ( 25 oC ). Can be found on module spec sheets and its often referred as: temperature coefficient of power or pmp. |
| derating_age_system | The factor by which the whole system will be derated per year since Install Date. It is used to calculate time dependent system loss. |
| derating_other_system | The total system losses as a fraction of the total energy output. This excludes shading, soiling, snow, inverter, temperature and age. Includes losses such as wiring. |
| inverter_peak_efficiency | The peak efficiency value in your inverter efficiency curve. It is used to scale the conversion efficiency of DC to AC, as a function of the inverter load. Must be at least 0.01 and at most 1. |
| tracker_axis_azimuth | The off north-south azimuth angle for a horizontal single axis tracking site. Most commonly this will be close to zero. North is 0, South is &#177;180, Eastward facing is negative values. Westward facing is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| tracker_max_rotation_angle | The maximum off-horizontal angle for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| tracker_back_tracking | Whether the trackers backtrack at low solar elevation angles, for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| tracker_smart_tracking | Whether the trackers move to horizontal during cloudy periods with zero DNI, for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules. |
| terrain_slope | The average terrain slope in degrees from horizontal of your site. A site with no terrain slope has a value of zero. |
| terrain_azimuth | The average terrain slope downhill direction. North is 0, South is &#177;180, Eastward is negative values. Westward is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules. |
| dust_soiling_average | The average proportion of module production lost due to dust soiling. The value entered should reflect the impact of cleaning activity at your site. It is used to calculate the module (DC) production. |
| bifacial_system | Bifacial systems have modules that produce solar power not only from the front, but also the rear side of the panel. Used to create additional parameters that will allow us to model your production more accurately. |
| site_ground_albedo | The proportion of the incident downward irradiance reflected by the ground surface in the area underneath the PV modules. Used to calculate local ground-reflected irradiance. Particularly important for bifacial systems. |
| bifaciality_factor | For bifacial modules, the module rear efficiency as a proportion of the front efficiency subject to the same irradiance. Used to calculate the module rear production for bifacial systems. |
| pvrow_height | The height of the module rows, in metres, measured from the ground to the row center or axis. Used to calculate the module rear incident irradiance and production for bifacial systems. |
| pvrow_width | The width of the module rows, in metres. This is the cross-section width of the entire PV row. Used to calculate the module rear incident irradiance and production for bifacial systems. |
| clearsky_zenith_coefficients | Coefficients used to model clear-sky solar zenith irradiance for the resource. |
| cloudy_zenith_coefficients | Coefficients used to model cloudy-sky solar zenith irradiance for the resource. |
| forecast_production_enabled | Indicates whether ML forecast production is enabled for this site (premium only). |
