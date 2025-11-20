
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Solcast.Models;
using Solcast.Utilities;

namespace Solcast.Clients
{
    public class ForecastClient : BaseClient
    {
        public ForecastClient()
        {
        }

        /// <summary>
        /// Get high-spec PV power forecasts for the requested site from the present up to 14 days ahead, derived from satellite (clouds and irradiance over non-polar continental areas, nowcasted for approx. four hours ahead) and numerical weather models (other data and longer horizons).
        /// </summary>
        /// <param name="hours">The number of hours to return in the response.</param>
        /// <param name="outputParameters">The output parameters to include in the response.</param>
        /// <param name="resourceId">The resource id of the resource.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format.</param>
        /// <param name="applyAvailability">Percentage of the site’s total AC (inverter) capacity that is currently generating or expected to be generating during the forecast request period. E.g. if you specify a 50% availability, your returned power will be half of what it otherwise would be.</param>
        /// <param name="applyConstraint">Constraint on site’s total AC production, applied as a cap in the same way as the metadata parameter Site Export Limit. This will constrain all Solcast power values to be no higher than the apply_constraint value you specify. If you need an unconstrained forecast, you should not use this parameter.</param>
        /// <param name="applyDustSoiling">A user-override for dust_soiling_average. If you specify this parameter in your API call, we will replace the site's annual or monthly average dust soiling values with the value you specify in your API call.E.g. if you specify a 0.7 dust soiling, your returned power will be reduced by 70%.</param>
        /// <param name="applySnowSoiling">A user-override for Solcast’s dynamic snow soiling, which is based on global snow cover and weather forecast data, and changes from hour to hour. If you specify this parameter in your API call (e.g. if snow clearing has just been performed), we will replace the Solcast dynamic hour to hour value with the single value you specify. E.g. if you specify a 0.7 snow soiling, your returned power will be reduced by 70%.</param>
        /// <param name="applyTrackerInactive">Indicating if trackers are inactive. If True, panels are assumed all facing up (i.e. zero rotation). Only has effect if your site has a tracking_type that is not “fixed”.</param>
        /// <param name="terrainShading">If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects.</param>
        /// <param name="format">Response format</param>
        public async Task<ApiResponse<ForecastsDataResponse>> GetForecastAdvancedPvPower(
            string resourceId,
            int? hours = null,
            List<string> outputParameters = null,
            string period = null,
            double? applyAvailability = null,
            double? applyConstraint = null,
            double? applyDustSoiling = null,
            double? applySnowSoiling = null,
            bool? applyTrackerInactive = null,
            bool? terrainShading = null,
            string format = null
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("resourceId", resourceId.ToString());
                if (hours.HasValue) parameters.Add("hours", hours.Value.ToString());
                if (outputParameters != null && outputParameters.Any()) parameters.Add("outputParameters", string.Join(",", outputParameters));
                if (period != null) parameters.Add("period", period.ToString());
                if (applyAvailability.HasValue) parameters.Add("applyAvailability", applyAvailability.Value.ToString());
                if (applyConstraint.HasValue) parameters.Add("applyConstraint", applyConstraint.Value.ToString());
                if (applyDustSoiling.HasValue) parameters.Add("applyDustSoiling", applyDustSoiling.Value.ToString());
                if (applySnowSoiling.HasValue) parameters.Add("applySnowSoiling", applySnowSoiling.Value.ToString());
                if (applyTrackerInactive.HasValue) parameters.Add("applyTrackerInactive", applyTrackerInactive.Value.ToString());
                if (terrainShading.HasValue) parameters.Add("terrainShading", terrainShading.Value.ToString());
                if (format != null) parameters.Add("format", format.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.ForecastAdvancedPvPower + $"?{queryString}");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedApiKeyException("The API key provided is invalid or unauthorized.");
                }

                response.EnsureSuccessStatusCode();

                var rawContent = await response.Content.ReadAsStringAsync();
                
                // Verbose output - useful for MCP scenarios and debugging
                var verboseFlag = Environment.GetEnvironmentVariable("SOLCAST_VERBOSE_OUTPUT");
                if (verboseFlag?.Equals("true", StringComparison.OrdinalIgnoreCase) == true)
                {
                    Console.Error.WriteLine("[Solcast] Raw Response: " + rawContent);
                }

                if (parameters.ContainsKey("format") && parameters["format"] == "json")
                {
                    var data = JsonConvert.DeserializeObject<ForecastsDataResponse>(rawContent);
                    return new ApiResponse<ForecastsDataResponse>(data, rawContent);
                }
                return new ApiResponse<ForecastsDataResponse>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "resourceId=" + resourceId + ", " + "hours=" + hours + ", " + "outputParameters=" + outputParameters + ", " + "period=" + period + ", " + "applyAvailability=" + applyAvailability + ", " + "applyConstraint=" + applyConstraint + ", " + "applyDustSoiling=" + applyDustSoiling + ", " + "applySnowSoiling=" + applySnowSoiling + ", " + "applyTrackerInactive=" + applyTrackerInactive + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetForecastAdvancedPvPower
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "resourceId=" + resourceId + ", " + "hours=" + hours + ", " + "outputParameters=" + outputParameters + ", " + "period=" + period + ", " + "applyAvailability=" + applyAvailability + ", " + "applyConstraint=" + applyConstraint + ", " + "applyDustSoiling=" + applyDustSoiling + ", " + "applySnowSoiling=" + applySnowSoiling + ", " + "applyTrackerInactive=" + applyTrackerInactive + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetForecastAdvancedPvPower
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Get forecast aggregation data for up to 14 days of data at a time for a requested collection or aggregation.
        /// </summary>
        /// <param name="outputParameters">The output parameters to include in the response.</param>
        /// <param name="collectionId">Unique identifier for your collection.</param>
        /// <param name="aggregationId">Unique identifier that belongs to the requested collection.</param>
        /// <param name="hours">The number of hours to return in the response.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format.</param>
        /// <param name="format">Response format</param>
        public async Task<ApiResponse<ForecastAggregationResponse>> GetForecastAggregations(
            List<string> outputParameters = null,
            string collectionId = null,
            string aggregationId = null,
            int? hours = null,
            string period = null,
            string format = null
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                if (outputParameters != null && outputParameters.Any()) parameters.Add("outputParameters", string.Join(",", outputParameters));
                if (collectionId != null) parameters.Add("collectionId", collectionId.ToString());
                if (aggregationId != null) parameters.Add("aggregationId", aggregationId.ToString());
                if (hours.HasValue) parameters.Add("hours", hours.Value.ToString());
                if (period != null) parameters.Add("period", period.ToString());
                if (format != null) parameters.Add("format", format.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.ForecastAggregations + $"?{queryString}");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedApiKeyException("The API key provided is invalid or unauthorized.");
                }

                response.EnsureSuccessStatusCode();

                var rawContent = await response.Content.ReadAsStringAsync();
                
                // Verbose output - useful for MCP scenarios and debugging
                var verboseFlag = Environment.GetEnvironmentVariable("SOLCAST_VERBOSE_OUTPUT");
                if (verboseFlag?.Equals("true", StringComparison.OrdinalIgnoreCase) == true)
                {
                    Console.Error.WriteLine("[Solcast] Raw Response: " + rawContent);
                }

                if (parameters.ContainsKey("format") && parameters["format"] == "json")
                {
                    var data = JsonConvert.DeserializeObject<ForecastAggregationResponse>(rawContent);
                    return new ApiResponse<ForecastAggregationResponse>(data, rawContent);
                }
                return new ApiResponse<ForecastAggregationResponse>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "outputParameters=" + outputParameters + ", " + "collectionId=" + collectionId + ", " + "aggregationId=" + aggregationId + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "format=" + format;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetForecastAggregations
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "outputParameters=" + outputParameters + ", " + "collectionId=" + collectionId + ", " + "aggregationId=" + aggregationId + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetForecastAggregations
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Get basic rooftop PV power forecasts from the present time up to 14 days ahead for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas, nowcasted for approx. four hours ahead) and numerical weather models (other data and longer horizons).
        /// 
        /// The basic rooftop power simulation is only suitable for residential and smaller C&I rooftop sites, not for grid-scale sites.
        /// 
        /// **Attention hobbyist users**
        /// 
        /// If you have a hobbyist user account please use the [Rooftop Sites (Hobbyist)](https://docs.solcast.com.au/#00577cf8-b43b-4349-b4b5-a5f063916f5a) endpoints.
        /// </summary>
        /// <param name="hours">The number of hours to return in the response.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format.</param>
        /// <param name="latitude">The latitude of the location you request data for. Must be a decimal number between -90 and 90.</param>
        /// <param name="longitude">The longitude of the location you request data for. Must be a decimal number between -180 and 180.</param>
        /// <param name="capacity">The capacity of the inverter (AC) or the modules (DC), whichever is greater, in kilowatts (kW).</param>
        /// <param name="tilt">The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90.</param>
        /// <param name="azimuth">The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere.</param>
        /// <param name="installDate">The date (yyyy-MM-dd) of installation of the PV system. We use this to estimate your loss_factor based on the ageing of your system. If you provide us with a loss_factor directly, we will ignore this date.</param>
        /// <param name="lossFactor">Default is 0.90 A factor to reduce your output forecast from the full capacity based on characteristics of the PV array or inverter. This is effectively the non-temperature loss effects on the nameplate rating of the PV system, including inefficiency and soiling. For a 1kW PV system anything that reduces 1000W/m2 solar radiation from producing 1000W of power output (assuming temperature is 25C). Valid values are between 0 and 1 (i.e. 0.6 equals 60%). If you specify 0.6 your returned power will be a maximum of 60% of AC capacity.</param>
        /// <param name="outputParameters">The output parameters to include in the response.</param>
        /// <param name="terrainShading">If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects.</param>
        /// <param name="format">Response format</param>
        public async Task<ApiResponse<ForecastsDataResponse>> GetForecastRooftopPvPower(
            double? latitude,
            double? longitude,
            float? capacity,
            int? hours = null,
            string period = null,
            float? tilt = null,
            float? azimuth = null,
            string installDate = null,
            float? lossFactor = null,
            List<string> outputParameters = null,
            bool? terrainShading = null,
            string format = null
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("latitude", latitude.ToString());
                parameters.Add("longitude", longitude.ToString());
                parameters.Add("capacity", capacity.ToString());
                if (hours.HasValue) parameters.Add("hours", hours.Value.ToString());
                if (period != null) parameters.Add("period", period.ToString());
                if (tilt.HasValue) parameters.Add("tilt", tilt.Value.ToString());
                if (azimuth.HasValue) parameters.Add("azimuth", azimuth.Value.ToString());
                if (installDate != null) parameters.Add("installDate", installDate.ToString());
                if (lossFactor.HasValue) parameters.Add("lossFactor", lossFactor.Value.ToString());
                if (outputParameters != null && outputParameters.Any()) parameters.Add("outputParameters", string.Join(",", outputParameters));
                if (terrainShading.HasValue) parameters.Add("terrainShading", terrainShading.Value.ToString());
                if (format != null) parameters.Add("format", format.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.ForecastRooftopPvPower + $"?{queryString}");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedApiKeyException("The API key provided is invalid or unauthorized.");
                }

                response.EnsureSuccessStatusCode();

                var rawContent = await response.Content.ReadAsStringAsync();
                
                // Verbose output - useful for MCP scenarios and debugging
                var verboseFlag = Environment.GetEnvironmentVariable("SOLCAST_VERBOSE_OUTPUT");
                if (verboseFlag?.Equals("true", StringComparison.OrdinalIgnoreCase) == true)
                {
                    Console.Error.WriteLine("[Solcast] Raw Response: " + rawContent);
                }

                if (parameters.ContainsKey("format") && parameters["format"] == "json")
                {
                    var data = JsonConvert.DeserializeObject<ForecastsDataResponse>(rawContent);
                    return new ApiResponse<ForecastsDataResponse>(data, rawContent);
                }
                return new ApiResponse<ForecastsDataResponse>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "capacity=" + capacity + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "tilt=" + tilt + ", " + "azimuth=" + azimuth + ", " + "installDate=" + installDate + ", " + "lossFactor=" + lossFactor + ", " + "outputParameters=" + outputParameters + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetForecastRooftopPvPower
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "capacity=" + capacity + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "tilt=" + tilt + ", " + "azimuth=" + azimuth + ", " + "installDate=" + installDate + ", " + "lossFactor=" + lossFactor + ", " + "outputParameters=" + outputParameters + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetForecastRooftopPvPower
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Get irradiance and weather forecasts for the requested location from the present up to 14 days ahead, derived from satellite (clouds and irradiance over non-polar continental areas, now casted for approx. four hours ahead) and numerical weather models (other data and longer horizons).
        /// </summary>
        /// <param name="hours">The number of hours to return in the response.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format.</param>
        /// <param name="tilt">The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90.</param>
        /// <param name="azimuth">The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere.</param>
        /// <param name="arrayType">The type of sun-tracking or geometry configuration of your site's modules.</param>
        /// <param name="outputParameters">The output parameters to include in the response.</param>
        /// <param name="terrainShading">If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects.</param>
        /// <param name="latitude">The latitude of the location you request data for. Must be a decimal number between -90 and 90.</param>
        /// <param name="longitude">The longitude of the location you request data for. Must be a decimal number between -180 and 180.</param>
        /// <param name="format">Response format</param>
        public async Task<ApiResponse<ForecastsDataResponse>> GetForecastRadiationAndWeather(
            double? latitude,
            double? longitude,
            int? hours = null,
            string period = null,
            float? tilt = null,
            float? azimuth = null,
            string arrayType = null,
            List<string> outputParameters = null,
            bool? terrainShading = null,
            string format = null
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("latitude", latitude.ToString());
                parameters.Add("longitude", longitude.ToString());
                if (hours.HasValue) parameters.Add("hours", hours.Value.ToString());
                if (period != null) parameters.Add("period", period.ToString());
                if (tilt.HasValue) parameters.Add("tilt", tilt.Value.ToString());
                if (azimuth.HasValue) parameters.Add("azimuth", azimuth.Value.ToString());
                if (arrayType != null) parameters.Add("arrayType", arrayType.ToString());
                if (outputParameters != null && outputParameters.Any()) parameters.Add("outputParameters", string.Join(",", outputParameters));
                if (terrainShading.HasValue) parameters.Add("terrainShading", terrainShading.Value.ToString());
                if (format != null) parameters.Add("format", format.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.ForecastRadiationAndWeather + $"?{queryString}");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedApiKeyException("The API key provided is invalid or unauthorized.");
                }

                response.EnsureSuccessStatusCode();

                var rawContent = await response.Content.ReadAsStringAsync();
                
                // Verbose output - useful for MCP scenarios and debugging
                var verboseFlag = Environment.GetEnvironmentVariable("SOLCAST_VERBOSE_OUTPUT");
                if (verboseFlag?.Equals("true", StringComparison.OrdinalIgnoreCase) == true)
                {
                    Console.Error.WriteLine("[Solcast] Raw Response: " + rawContent);
                }

                if (parameters.ContainsKey("format") && parameters["format"] == "json")
                {
                    var data = JsonConvert.DeserializeObject<ForecastsDataResponse>(rawContent);
                    return new ApiResponse<ForecastsDataResponse>(data, rawContent);
                }
                return new ApiResponse<ForecastsDataResponse>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "tilt=" + tilt + ", " + "azimuth=" + azimuth + ", " + "arrayType=" + arrayType + ", " + "outputParameters=" + outputParameters + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetForecastRadiationAndWeather
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "tilt=" + tilt + ", " + "azimuth=" + azimuth + ", " + "arrayType=" + arrayType + ", " + "outputParameters=" + outputParameters + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetForecastRadiationAndWeather
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <param name="latitude">The latitude of the location you request data for. Must be a decimal number between -90 and 90.</param>
        /// <param name="longitude">The longitude of the location you request data for. Must be a decimal number between -180 and 180.</param>
        /// <param name="timeZone">Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset. Default is utc.</param>
        /// <param name="hours">The number of hours to return in the response. Default is 24.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format. Default is PT30M.</param>
        /// <param name="depoVelocPm10">Deposition or settling velocity of PM2.5 particulates. [m/s]. Default is 0.0009.</param>
        /// <param name="depoVelocPm25">Deposition or settling velocity of PM10 particulates. [m/s]. Default is 0.004.</param>
        /// <param name="pm10">Restricted. A list of PM10 values. Concentration of airborne particulate matter (PM) with aerodynamicdiameter less than 10 microns. [g/m^3]. HSU endpoint will internally use Solcast's PM2.5 values tailored to your request time period.</param>
        /// <param name="pm25">Restricted. Concentration of airborne particulate matter (PM) with aerodynamic diameter less than 2.5 microns. [g/m^3]. HSU endpoint will internally use Solcast's PM2.5 values tailored to your request time period.</param>
        /// <param name="tilt">The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90.</param>
        /// <param name="cleaningThreshold">Amount of daily rainfall required to clean the panels (mm). Default is 6.0.</param>
        /// <param name="initialSoiling">Initial percentage of energy lost due to soiling at time zero in the rainfall series input. If not provided, Solcast will perform a ramp up series calculation to accurately determine this value. If provided must be >= 0 and < 0.3437.</param>
        /// <param name="rainAccumPeriod">Restricted. Specifies the period of time to sum rain fall data. Internally this value is set to 24 hours in line with the findings of https://ieeexplore.ieee.org/document/4060159 .</param>
        /// <param name="rainfall">Restricted. List of rainfall values. HSU endpoint will internally use Solcast's percepitation rate values tailored to your request time period.</param>
        /// <param name="manualWashDates">A list of ISO_8601 compliant dates or a repeating interval when manual cleaning of the panels occurred. A list of dates example: [2025-01-01,2025-01-05,2025-01-10]. A repeating interval example: R3/2025-01-01T00:00:00Z/P14D. Wash dates outside of the start and end of the request are discarded.</param>
        /// <param name="format">Response format (json, csv). Default is json.</param>
        public async Task<ApiResponse<ForecastsDataResponse>> GetForecastHsu(
            double? latitude,
            double? longitude,
            string timeZone = null,
            int? hours = null,
            string period = null,
            double? depoVelocPm10 = null,
            double? depoVelocPm25 = null,
            List<double?> pm10 = null,
            List<double?> pm25 = null,
            double? tilt = null,
            double? cleaningThreshold = null,
            double? initialSoiling = null,
            int? rainAccumPeriod = null,
            List<double?> rainfall = null,
            List<string> manualWashDates = null,
            string format = null
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("latitude", latitude.ToString());
                parameters.Add("longitude", longitude.ToString());
                if (timeZone != null) parameters.Add("timeZone", timeZone.ToString());
                if (hours.HasValue) parameters.Add("hours", hours.Value.ToString());
                if (period != null) parameters.Add("period", period.ToString());
                if (depoVelocPm10.HasValue) parameters.Add("depoVelocPm10", depoVelocPm10.Value.ToString());
                if (depoVelocPm25.HasValue) parameters.Add("depoVelocPm25", depoVelocPm25.Value.ToString());
                if (pm10 != null) parameters.Add("pm10", pm10.ToString());
                if (pm25 != null) parameters.Add("pm25", pm25.ToString());
                if (tilt.HasValue) parameters.Add("tilt", tilt.Value.ToString());
                if (cleaningThreshold.HasValue) parameters.Add("cleaningThreshold", cleaningThreshold.Value.ToString());
                if (initialSoiling.HasValue) parameters.Add("initialSoiling", initialSoiling.Value.ToString());
                if (rainAccumPeriod.HasValue) parameters.Add("rainAccumPeriod", rainAccumPeriod.Value.ToString());
                if (rainfall != null) parameters.Add("rainfall", rainfall.ToString());
                if (manualWashDates != null && manualWashDates.Any()) parameters.Add("manualWashDates", string.Join(",", manualWashDates));
                if (format != null) parameters.Add("format", format.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.ForecastSoilingHsu + $"?{queryString}");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedApiKeyException("The API key provided is invalid or unauthorized.");
                }

                response.EnsureSuccessStatusCode();

                var rawContent = await response.Content.ReadAsStringAsync();
                
                // Verbose output - useful for MCP scenarios and debugging
                var verboseFlag = Environment.GetEnvironmentVariable("SOLCAST_VERBOSE_OUTPUT");
                if (verboseFlag?.Equals("true", StringComparison.OrdinalIgnoreCase) == true)
                {
                    Console.Error.WriteLine("[Solcast] Raw Response: " + rawContent);
                }

                if (parameters.ContainsKey("format") && parameters["format"] == "json")
                {
                    var data = JsonConvert.DeserializeObject<ForecastsDataResponse>(rawContent);
                    return new ApiResponse<ForecastsDataResponse>(data, rawContent);
                }
                return new ApiResponse<ForecastsDataResponse>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "timeZone=" + timeZone + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "depoVelocPm10=" + depoVelocPm10 + ", " + "depoVelocPm25=" + depoVelocPm25 + ", " + "pm10=" + pm10 + ", " + "pm25=" + pm25 + ", " + "tilt=" + tilt + ", " + "cleaningThreshold=" + cleaningThreshold + ", " + "initialSoiling=" + initialSoiling + ", " + "rainAccumPeriod=" + rainAccumPeriod + ", " + "rainfall=" + rainfall + ", " + "manualWashDates=" + manualWashDates + ", " + "format=" + format;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetForecastHsu
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "timeZone=" + timeZone + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "depoVelocPm10=" + depoVelocPm10 + ", " + "depoVelocPm25=" + depoVelocPm25 + ", " + "pm10=" + pm10 + ", " + "pm25=" + pm25 + ", " + "tilt=" + tilt + ", " + "cleaningThreshold=" + cleaningThreshold + ", " + "initialSoiling=" + initialSoiling + ", " + "rainAccumPeriod=" + rainAccumPeriod + ", " + "rainfall=" + rainfall + ", " + "manualWashDates=" + manualWashDates + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetForecastHsu
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <param name="latitude">The latitude of the location you request data for. Must be a decimal number between -90 and 90.</param>
        /// <param name="longitude">The longitude of the location you request data for. Must be a decimal number between -180 and 180.</param>
        /// <param name="timeZone">Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset. Default is utc.</param>
        /// <param name="hours">The number of hours to return in the response. Default is 24.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format. Default is PT30M.</param>
        /// <param name="cleaningThreshold">Amount of daily rainfall required to clean the panels (mm). Default is 6.0.</param>
        /// <param name="soilingLossRate">Percentage of energy lost due to one day of soiling.</param>
        /// <param name="gracePeriod">Number of days after a rainfall event when it’s assumed the ground is damp, and so it’s assumed there is no soiling.</param>
        /// <param name="maxSoiling">Maximum percentage of energy lost due to soiling. Soiling will build up until this value.</param>
        /// <param name="initialSoiling">Initial percentage of energy lost due to soiling at time zero in the rainfall series input. If not provided, Solcast will perform a ramp up series calculation to accurately determine this value.</param>
        /// <param name="manualWashDates">A list of ISO_8601 compliant dates or a repeating interval when manual cleaning of the panels occurred. A list of dates example: [2025-01-01,2025-01-05,2025-01-10]. A repeating interval example: R3/2025-01-01T00:00:00Z/P14D. Wash dates outside of the start and end of the request are discarded.</param>
        /// <param name="format">Response format (json, csv). Default is json.</param>
        public async Task<ApiResponse<ForecastsDataResponse>> GetForecastKimber(
            double? latitude,
            double? longitude,
            string timeZone = null,
            int? hours = null,
            string period = null,
            double? cleaningThreshold = null,
            double? soilingLossRate = null,
            int? gracePeriod = null,
            double? maxSoiling = null,
            double? initialSoiling = null,
            List<string> manualWashDates = null,
            string format = null
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("latitude", latitude.ToString());
                parameters.Add("longitude", longitude.ToString());
                if (timeZone != null) parameters.Add("timeZone", timeZone.ToString());
                if (hours.HasValue) parameters.Add("hours", hours.Value.ToString());
                if (period != null) parameters.Add("period", period.ToString());
                if (cleaningThreshold.HasValue) parameters.Add("cleaningThreshold", cleaningThreshold.Value.ToString());
                if (soilingLossRate.HasValue) parameters.Add("soilingLossRate", soilingLossRate.Value.ToString());
                if (gracePeriod.HasValue) parameters.Add("gracePeriod", gracePeriod.Value.ToString());
                if (maxSoiling.HasValue) parameters.Add("maxSoiling", maxSoiling.Value.ToString());
                if (initialSoiling.HasValue) parameters.Add("initialSoiling", initialSoiling.Value.ToString());
                if (manualWashDates != null && manualWashDates.Any()) parameters.Add("manualWashDates", string.Join(",", manualWashDates));
                if (format != null) parameters.Add("format", format.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.ForecastSoilingKimber + $"?{queryString}");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedApiKeyException("The API key provided is invalid or unauthorized.");
                }

                response.EnsureSuccessStatusCode();

                var rawContent = await response.Content.ReadAsStringAsync();
                
                // Verbose output - useful for MCP scenarios and debugging
                var verboseFlag = Environment.GetEnvironmentVariable("SOLCAST_VERBOSE_OUTPUT");
                if (verboseFlag?.Equals("true", StringComparison.OrdinalIgnoreCase) == true)
                {
                    Console.Error.WriteLine("[Solcast] Raw Response: " + rawContent);
                }

                if (parameters.ContainsKey("format") && parameters["format"] == "json")
                {
                    var data = JsonConvert.DeserializeObject<ForecastsDataResponse>(rawContent);
                    return new ApiResponse<ForecastsDataResponse>(data, rawContent);
                }
                return new ApiResponse<ForecastsDataResponse>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "timeZone=" + timeZone + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "cleaningThreshold=" + cleaningThreshold + ", " + "soilingLossRate=" + soilingLossRate + ", " + "gracePeriod=" + gracePeriod + ", " + "maxSoiling=" + maxSoiling + ", " + "initialSoiling=" + initialSoiling + ", " + "manualWashDates=" + manualWashDates + ", " + "format=" + format;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetForecastKimber
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "timeZone=" + timeZone + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "cleaningThreshold=" + cleaningThreshold + ", " + "soilingLossRate=" + soilingLossRate + ", " + "gracePeriod=" + gracePeriod + ", " + "maxSoiling=" + maxSoiling + ", " + "initialSoiling=" + initialSoiling + ", " + "manualWashDates=" + manualWashDates + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetForecastKimber
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }    }
}
