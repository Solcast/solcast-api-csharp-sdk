
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
    public class HistoricClient : BaseClient
    {
        public HistoricClient()
        {
        }

        /// <summary>
        /// Get historical advanced PV power estimated actuals for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). Data is available from 2007-01-01T00:00Z to 7 days ago.
        /// 
        /// [The period from 7 days ago to Now is available in the Get Advanced PV Power Live Estimated Actuals endpoint.](https://docs.solcast.com.au/#9f3aed26-1078-4ff6-86e6-23a710c6fae7)
        /// </summary>
        /// <param name="start">ISO_8601 compliant starting datetime for the historical data. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed.</param>
        /// <param name="end">Must include one of end_date and duration. ISO_8601 compliant ending datetime for the historical data. Must be within 31 days of the start_date. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed.</param>
        /// <param name="duration">Must include one of end_date and duration. ISO_8601 compliant duration for the historical data. Must be within 31 days of the start_date.</param>
        /// <param name="timeZone">Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset.</param>
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
        public async Task<ApiResponse<LiveResponse>> GetHistoricAdvancedPvPower(
            string start,
            string resourceId,
            string end = null,
            string duration = null,
            string timeZone = null,
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
                parameters.Add("start", start.ToString());
                parameters.Add("resourceId", resourceId.ToString());
                if (end != null) parameters.Add("end", end.ToString());
                if (duration != null) parameters.Add("duration", duration.ToString());
                if (timeZone != null) parameters.Add("timeZone", timeZone.ToString());
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
                var response = await _httpClient.GetAsync(SolcastUrls.HistoricAdvancedPvPower + $"?{queryString}");

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
                    var data = JsonConvert.DeserializeObject<LiveResponse>(rawContent);
                    return new ApiResponse<LiveResponse>(data, rawContent);
                }
                return new ApiResponse<LiveResponse>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "start=" + start + ", " + "resourceId=" + resourceId + ", " + "end=" + end + ", " + "duration=" + duration + ", " + "timeZone=" + timeZone + ", " + "outputParameters=" + outputParameters + ", " + "period=" + period + ", " + "applyAvailability=" + applyAvailability + ", " + "applyConstraint=" + applyConstraint + ", " + "applyDustSoiling=" + applyDustSoiling + ", " + "applySnowSoiling=" + applySnowSoiling + ", " + "applyTrackerInactive=" + applyTrackerInactive + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetHistoricAdvancedPvPower
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "start=" + start + ", " + "resourceId=" + resourceId + ", " + "end=" + end + ", " + "duration=" + duration + ", " + "timeZone=" + timeZone + ", " + "outputParameters=" + outputParameters + ", " + "period=" + period + ", " + "applyAvailability=" + applyAvailability + ", " + "applyConstraint=" + applyConstraint + ", " + "applyDustSoiling=" + applyDustSoiling + ", " + "applySnowSoiling=" + applySnowSoiling + ", " + "applyTrackerInactive=" + applyTrackerInactive + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetHistoricAdvancedPvPower
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Get historical irradiance and weather estimated actuals for up to 31 days of data at a time for a requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). Data is available from 2007-01-01T00:00Z to 7 days ago.
        /// 
        /// [The period from 7 days ago to Now is available in the Get Irradiance and Weather Live Estimated Actuals endpoint.](https://docs.solcast.com.au/#b9863910-c788-4e98-a3af-eb8da8f49647)
        /// </summary>
        /// <param name="start">ISO_8601 compliant starting datetime for the historical data. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed.</param>
        /// <param name="end">Must include one of end_date and duration. ISO_8601 compliant ending datetime for the historical data. Must be within 31 days of the start_date. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed.</param>
        /// <param name="duration">Must include one of end_date and duration. ISO_8601 compliant duration for the historical data. Must be within 31 days of the start_date.</param>
        /// <param name="timeZone">Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format.</param>
        /// <param name="tilt">The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90.</param>
        /// <param name="azimuth">The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere.</param>
        /// <param name="arrayType">The type of sun-tracking or geometry configuration of your site's modules.</param>
        /// <param name="outputParameters">The output parameters to include in the response.</param>
        /// <param name="terrainShading">If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects.</param>
        /// <param name="latitude">The latitude of the location you request data for. Must be a decimal number between -90 and 90.</param>
        /// <param name="longitude">The longitude of the location you request data for. Must be a decimal number between -180 and 180.</param>
        /// <param name="format">Response format</param>
        public async Task<ApiResponse<HistoricRadiationAndWeatherResponse>> GetHistoricRadiationAndWeather(
            string start,
            double? latitude,
            double? longitude,
            string end = null,
            string duration = null,
            string timeZone = null,
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
                parameters.Add("start", start.ToString());
                parameters.Add("latitude", latitude.ToString());
                parameters.Add("longitude", longitude.ToString());
                if (end != null) parameters.Add("end", end.ToString());
                if (duration != null) parameters.Add("duration", duration.ToString());
                if (timeZone != null) parameters.Add("timeZone", timeZone.ToString());
                if (period != null) parameters.Add("period", period.ToString());
                if (tilt.HasValue) parameters.Add("tilt", tilt.Value.ToString());
                if (azimuth.HasValue) parameters.Add("azimuth", azimuth.Value.ToString());
                if (arrayType != null) parameters.Add("arrayType", arrayType.ToString());
                if (outputParameters != null && outputParameters.Any()) parameters.Add("outputParameters", string.Join(",", outputParameters));
                if (terrainShading.HasValue) parameters.Add("terrainShading", terrainShading.Value.ToString());
                if (format != null) parameters.Add("format", format.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.HistoricRadiationAndWeather + $"?{queryString}");

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
                    var data = JsonConvert.DeserializeObject<HistoricRadiationAndWeatherResponse>(rawContent);
                    return new ApiResponse<HistoricRadiationAndWeatherResponse>(data, rawContent);
                }
                return new ApiResponse<HistoricRadiationAndWeatherResponse>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "start=" + start + ", " + "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "end=" + end + ", " + "duration=" + duration + ", " + "timeZone=" + timeZone + ", " + "period=" + period + ", " + "tilt=" + tilt + ", " + "azimuth=" + azimuth + ", " + "arrayType=" + arrayType + ", " + "outputParameters=" + outputParameters + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetHistoricRadiationAndWeather
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "start=" + start + ", " + "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "end=" + end + ", " + "duration=" + duration + ", " + "timeZone=" + timeZone + ", " + "period=" + period + ", " + "tilt=" + tilt + ", " + "azimuth=" + azimuth + ", " + "arrayType=" + arrayType + ", " + "outputParameters=" + outputParameters + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetHistoricRadiationAndWeather
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Get historical basic rooftop PV power estimated actuals for the requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). Data is available from 2007-01-01T00:00Z to 7 days ago.
        /// 
        /// [The period from 7 days ago to Now is available in the Get Rooftop PV Power Live Estimated Actuals endpoint.](https://docs.solcast.com.au/#4c9fa796-82e5-4e8a-b811-85a8c9fb85db)
        /// 
        /// **Attention hobbyist users**
        /// 
        /// If you have a hobbyist user account please use the [Rooftop Sites (Hobbyist)](https://docs.solcast.com.au/#00577cf8-b43b-4349-b4b5-a5f063916f5a) endpoints.
        /// </summary>
        /// <param name="start">ISO_8601 compliant starting datetime for the historical data. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed.</param>
        /// <param name="end">Must include one of end_date and duration. ISO_8601 compliant ending datetime for the historical data. Must be within 31 days of the start_date. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed.</param>
        /// <param name="duration">Must include one of end_date and duration. ISO_8601 compliant duration for the historical data. Must be within 31 days of the start_date.</param>
        /// <param name="timeZone">Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset.</param>
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
        public async Task<ApiResponse<HistoricPvPowerResponse>> GetHistoricRooftopPvPower(
            string start,
            double? latitude,
            double? longitude,
            float? capacity,
            string end = null,
            string duration = null,
            string timeZone = null,
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
                parameters.Add("start", start.ToString());
                parameters.Add("latitude", latitude.ToString());
                parameters.Add("longitude", longitude.ToString());
                parameters.Add("capacity", capacity.ToString());
                if (end != null) parameters.Add("end", end.ToString());
                if (duration != null) parameters.Add("duration", duration.ToString());
                if (timeZone != null) parameters.Add("timeZone", timeZone.ToString());
                if (period != null) parameters.Add("period", period.ToString());
                if (tilt.HasValue) parameters.Add("tilt", tilt.Value.ToString());
                if (azimuth.HasValue) parameters.Add("azimuth", azimuth.Value.ToString());
                if (installDate != null) parameters.Add("installDate", installDate.ToString());
                if (lossFactor.HasValue) parameters.Add("lossFactor", lossFactor.Value.ToString());
                if (outputParameters != null && outputParameters.Any()) parameters.Add("outputParameters", string.Join(",", outputParameters));
                if (terrainShading.HasValue) parameters.Add("terrainShading", terrainShading.Value.ToString());
                if (format != null) parameters.Add("format", format.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.HistoricRooftopPvPower + $"?{queryString}");

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
                    var data = JsonConvert.DeserializeObject<HistoricPvPowerResponse>(rawContent);
                    return new ApiResponse<HistoricPvPowerResponse>(data, rawContent);
                }
                return new ApiResponse<HistoricPvPowerResponse>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "start=" + start + ", " + "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "capacity=" + capacity + ", " + "end=" + end + ", " + "duration=" + duration + ", " + "timeZone=" + timeZone + ", " + "period=" + period + ", " + "tilt=" + tilt + ", " + "azimuth=" + azimuth + ", " + "installDate=" + installDate + ", " + "lossFactor=" + lossFactor + ", " + "outputParameters=" + outputParameters + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetHistoricRooftopPvPower
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "start=" + start + ", " + "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "capacity=" + capacity + ", " + "end=" + end + ", " + "duration=" + duration + ", " + "timeZone=" + timeZone + ", " + "period=" + period + ", " + "tilt=" + tilt + ", " + "azimuth=" + azimuth + ", " + "installDate=" + installDate + ", " + "lossFactor=" + lossFactor + ", " + "outputParameters=" + outputParameters + ", " + "terrainShading=" + terrainShading + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetHistoricRooftopPvPower
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Get historical soiling loss using the Kimber model for up to 31 days of data at a time for a requested location. Data is available from 2007-01-01T00:00Z to 7 days ago.
        /// </summary>
        /// <param name="latitude">The latitude of the location you request data for. Must be a decimal number between -90 and 90.</param>
        /// <param name="longitude">The longitude of the location you request data for. Must be a decimal number between -180 and 180.</param>
        /// <param name="timeZone">Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format.</param>
        /// <param name="start">ISO_8601 compliant starting datetime for the historical data. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed.</param>
        /// <param name="duration">Must include one of end_date and duration. ISO_8601 compliant duration for the historical data. Must be within 31 days of the start_date.</param>
        /// <param name="end">Must include one of end_date and duration. ISO_8601 compliant ending datetime for the historical data. Must be within 31 days of the start_date. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed.</param>
        /// <param name="cleaningThreshold">Amount of daily rainfall required to clean the panels (mm)</param>
        /// <param name="soilingLossRatePercent">Percentage of energy lost due to one day of soiling.</param>
        /// <param name="gracePeriod">Number of days after a rainfall event when it’s assumed the ground is damp, and so it’s assumed there is no soiling.</param>
        /// <param name="maxSoilingPercent">Maximum percentage of energy lost due to soiling. Soiling will build up until this value.</param>
        /// <param name="initialSoilingPercent">Initial percentage of energy lost due to soiling at time zero in the rainfall series input.</param>
        /// <param name="manualWashDates">A list of ISO 8601 compliant dates or a repeating interval when manual cleaning of the panels occurred.</param>
        /// <param name="format">Response format</param>
        public async Task<ApiResponse<EstimatedActualsDataResponse>> GetHistoricKimber(
            double? latitude,
            double? longitude,
            string start,
            string timeZone = null,
            string period = null,
            string duration = null,
            string end = null,
            int? cleaningThreshold = null,
            double? soilingLossRatePercent = null,
            int? gracePeriod = null,
            double? maxSoilingPercent = null,
            double? initialSoilingPercent = null,
            List<string> manualWashDates = null,
            string format = null
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("latitude", latitude.ToString());
                parameters.Add("longitude", longitude.ToString());
                parameters.Add("start", start.ToString());
                if (timeZone != null) parameters.Add("timeZone", timeZone.ToString());
                if (period != null) parameters.Add("period", period.ToString());
                if (duration != null) parameters.Add("duration", duration.ToString());
                if (end != null) parameters.Add("end", end.ToString());
                if (cleaningThreshold.HasValue) parameters.Add("cleaningThreshold", cleaningThreshold.Value.ToString());
                if (soilingLossRatePercent.HasValue) parameters.Add("soilingLossRatePercent", soilingLossRatePercent.Value.ToString());
                if (gracePeriod.HasValue) parameters.Add("gracePeriod", gracePeriod.Value.ToString());
                if (maxSoilingPercent.HasValue) parameters.Add("maxSoilingPercent", maxSoilingPercent.Value.ToString());
                if (initialSoilingPercent.HasValue) parameters.Add("initialSoilingPercent", initialSoilingPercent.Value.ToString());
                if (manualWashDates != null && manualWashDates.Any()) parameters.Add("manualWashDates", string.Join(",", manualWashDates));
                if (format != null) parameters.Add("format", format.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.HistoricSoilingKimber + $"?{queryString}");

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
                    var data = JsonConvert.DeserializeObject<EstimatedActualsDataResponse>(rawContent);
                    return new ApiResponse<EstimatedActualsDataResponse>(data, rawContent);
                }
                return new ApiResponse<EstimatedActualsDataResponse>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "start=" + start + ", " + "timeZone=" + timeZone + ", " + "period=" + period + ", " + "duration=" + duration + ", " + "end=" + end + ", " + "cleaningThreshold=" + cleaningThreshold + ", " + "soilingLossRatePercent=" + soilingLossRatePercent + ", " + "gracePeriod=" + gracePeriod + ", " + "maxSoilingPercent=" + maxSoilingPercent + ", " + "initialSoilingPercent=" + initialSoilingPercent + ", " + "manualWashDates=" + manualWashDates + ", " + "format=" + format;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetHistoricKimber
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "latitude=" + latitude + ", " + "longitude=" + longitude + ", " + "start=" + start + ", " + "timeZone=" + timeZone + ", " + "period=" + period + ", " + "duration=" + duration + ", " + "end=" + end + ", " + "cleaningThreshold=" + cleaningThreshold + ", " + "soilingLossRatePercent=" + soilingLossRatePercent + ", " + "gracePeriod=" + gracePeriod + ", " + "maxSoilingPercent=" + maxSoilingPercent + ", " + "initialSoilingPercent=" + initialSoilingPercent + ", " + "manualWashDates=" + manualWashDates + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetHistoricKimber
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }    }
}
