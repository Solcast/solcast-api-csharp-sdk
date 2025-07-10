
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
    public class TmyClient : BaseClient
    {
        public TmyClient()
        {
        }

        /// <summary>
        /// Get the irradiance and weather for a Typical Meteorological Year (TMY) at a requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). The TMY is calculated with data from 2007 to 2023.
        /// </summary>
        /// <param name="timeZone">Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format.</param>
        /// <param name="ghiWeight">When creating the TMY, the weighting of GHI to use in the target parameter. Note that ghi_weight + dni_weight must equal 1.</param>
        /// <param name="dniWeight">When creating the TMY, the weighting of DNI to use in the target parameter. Note that ghi_weight + dni_weight must equal 1.</param>
        /// <param name="probability">The probability percentile for the TMY. Allowed values are p50, p75, p90, p95.</param>
        /// <param name="outputParameters">The output parameters to include in the response.</param>
        /// <param name="resourceId">The resource id of the resource.</param>
        /// <param name="applyAvailability">Percentage of the site’s total AC (inverter) capacity that is currently generating or expected to be generating during the forecast request period. E.g. if you specify a 50% availability, your returned power will be half of what it otherwise would be.</param>
        /// <param name="applyConstraint">Constraint on site’s total AC production, applied as a cap in the same way as the metadata parameter Site Export Limit. This will constrain all Solcast power values to be no higher than the apply_constraint value you specify. If you need an unconstrained forecast, you should not use this parameter.</param>
        /// <param name="applyDustSoiling">A user-override for dust_soiling_average. If you specify this parameter in your API call, we will replace the site's annual or monthly average dust soiling values with the value you specify in your API call.E.g. if you specify a 0.7 dust soiling, your returned power will be reduced by 70%.</param>
        /// <param name="applySnowSoiling">A user-override for Solcast’s dynamic snow soiling, which is based on global snow cover and weather forecast data, and changes from hour to hour. If you specify this parameter in your API call (e.g. if snow clearing has just been performed), we will replace the Solcast dynamic hour to hour value with the single value you specify. E.g. if you specify a 0.7 snow soiling, your returned power will be reduced by 70%.</param>
        /// <param name="applyTrackerInactive">Indicating if trackers are inactive. If True, panels are assumed all facing up (i.e. zero rotation). Only has effect if your site has a tracking_type that is not “fixed”.</param>
        /// <param name="terrainShading">If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects.</param>
        /// <param name="format">Response format</param>
        public async Task<ApiResponse<TmyAdvancedPvPowerResponse>> GetAdvancedPvPower(
            string resourceId,
            string timeZone = null,
            string period = null,
            double? ghiWeight = null,
            double? dniWeight = null,
            string probability = null,
            List<string> outputParameters = null,
            double? applyAvailability = null,
            double? applyConstraint = null,
            double? applyDustSoiling = null,
            double? applySnowSoiling = null,
            bool? applyTrackerInactive = null,
            bool? terrainShading = null,
            string format = null
        )
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("resourceId", resourceId.ToString());
            if (timeZone != null) parameters.Add("timeZone", timeZone.ToString());
            if (period != null) parameters.Add("period", period.ToString());
            if (ghiWeight.HasValue) parameters.Add("ghiWeight", ghiWeight.Value.ToString());
            if (dniWeight.HasValue) parameters.Add("dniWeight", dniWeight.Value.ToString());
            if (probability != null) parameters.Add("probability", probability.ToString());
            if (outputParameters != null && outputParameters.Any()) parameters.Add("outputParameters", string.Join(",", outputParameters));
            if (applyAvailability.HasValue) parameters.Add("applyAvailability", applyAvailability.Value.ToString());
            if (applyConstraint.HasValue) parameters.Add("applyConstraint", applyConstraint.Value.ToString());
            if (applyDustSoiling.HasValue) parameters.Add("applyDustSoiling", applyDustSoiling.Value.ToString());
            if (applySnowSoiling.HasValue) parameters.Add("applySnowSoiling", applySnowSoiling.Value.ToString());
            if (applyTrackerInactive.HasValue) parameters.Add("applyTrackerInactive", applyTrackerInactive.Value.ToString());
            if (terrainShading.HasValue) parameters.Add("terrainShading", terrainShading.Value.ToString());
            if (format != null) parameters.Add("format", format.ToString());

            var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
            var response = await _httpClient.GetAsync(SolcastUrls.TmyAdvancedPvPower + $"?{queryString}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedApiKeyException("The API key provided is invalid or unauthorized.");
            }

            response.EnsureSuccessStatusCode();

            var rawContent = await response.Content.ReadAsStringAsync();

            if (parameters.ContainsKey("format") && parameters["format"] == "json")
            {
                var data = JsonConvert.DeserializeObject<TmyAdvancedPvPowerResponse>(rawContent);
                return new ApiResponse<TmyAdvancedPvPowerResponse>(data, rawContent);
            }
            return new ApiResponse<TmyAdvancedPvPowerResponse>(null, rawContent);
        }

        /// <summary>
        /// Get the irradiance and weather for a Typical Meteorological Year (TMY) at a requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). The TMY is calculated with data from 2007 to 2023.
        /// </summary>
        /// <param name="timeZone">Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format.</param>
        /// <param name="ghiWeight">When creating the TMY, the weighting of GHI to use in the target parameter. Note that ghi_weight + dni_weight must equal 1.</param>
        /// <param name="dniWeight">When creating the TMY, the weighting of DNI to use in the target parameter. Note that ghi_weight + dni_weight must equal 1.</param>
        /// <param name="probability">The probability percentile for the TMY. Allowed values are p50, p75, p90, p95.</param>
        /// <param name="tilt">The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90.</param>
        /// <param name="azimuth">The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere.</param>
        /// <param name="arrayType">The type of sun-tracking or geometry configuration of your site's modules.</param>
        /// <param name="outputParameters">The output parameters to include in the response.</param>
        /// <param name="terrainShading">If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects.</param>
        /// <param name="latitude">The latitude of the location you request data for. Must be a decimal number between -90 and 90.</param>
        /// <param name="longitude">The longitude of the location you request data for. Must be a decimal number between -180 and 180.</param>
        /// <param name="format">Response format</param>
        public async Task<ApiResponse<TmyRadiationAndWeatherResponse>> GetRadiationAndWeather(
            double? latitude,
            double? longitude,
            string timeZone = null,
            string period = null,
            double? ghiWeight = null,
            double? dniWeight = null,
            string probability = null,
            float? tilt = null,
            float? azimuth = null,
            string arrayType = null,
            List<string> outputParameters = null,
            bool? terrainShading = null,
            string format = null
        )
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("latitude", latitude.ToString());
            parameters.Add("longitude", longitude.ToString());
            if (timeZone != null) parameters.Add("timeZone", timeZone.ToString());
            if (period != null) parameters.Add("period", period.ToString());
            if (ghiWeight.HasValue) parameters.Add("ghiWeight", ghiWeight.Value.ToString());
            if (dniWeight.HasValue) parameters.Add("dniWeight", dniWeight.Value.ToString());
            if (probability != null) parameters.Add("probability", probability.ToString());
            if (tilt.HasValue) parameters.Add("tilt", tilt.Value.ToString());
            if (azimuth.HasValue) parameters.Add("azimuth", azimuth.Value.ToString());
            if (arrayType != null) parameters.Add("arrayType", arrayType.ToString());
            if (outputParameters != null && outputParameters.Any()) parameters.Add("outputParameters", string.Join(",", outputParameters));
            if (terrainShading.HasValue) parameters.Add("terrainShading", terrainShading.Value.ToString());
            if (format != null) parameters.Add("format", format.ToString());

            var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
            var response = await _httpClient.GetAsync(SolcastUrls.TmyRadiationAndWeather + $"?{queryString}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedApiKeyException("The API key provided is invalid or unauthorized.");
            }

            response.EnsureSuccessStatusCode();

            var rawContent = await response.Content.ReadAsStringAsync();

            if (parameters.ContainsKey("format") && parameters["format"] == "json")
            {
                var data = JsonConvert.DeserializeObject<TmyRadiationAndWeatherResponse>(rawContent);
                return new ApiResponse<TmyRadiationAndWeatherResponse>(data, rawContent);
            }
            return new ApiResponse<TmyRadiationAndWeatherResponse>(null, rawContent);
        }

        /// <summary>
        /// Get the basic rooftop PV power estimated actuals for a Typical Meteorological Year (TMY) at a requested location, derived from satellite (clouds and irradiance over non-polar continental areas) and numerical weather models (other data). The TMY is calculated with data from 2007 to 2023.
        /// </summary>
        /// <param name="timeZone">Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format.</param>
        /// <param name="ghiWeight">When creating the TMY, the weighting of GHI to use in the target parameter. Note that ghi_weight + dni_weight must equal 1.</param>
        /// <param name="dniWeight">When creating the TMY, the weighting of DNI to use in the target parameter. Note that ghi_weight + dni_weight must equal 1.</param>
        /// <param name="probability">The probability percentile for the TMY. Allowed values are p50, p75, p90, p95.</param>
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
        public async Task<ApiResponse<TmyRooftopPvPowerResponse>> GetRooftopPvPower(
            double? latitude,
            double? longitude,
            float? capacity,
            string timeZone = null,
            string period = null,
            double? ghiWeight = null,
            double? dniWeight = null,
            string probability = null,
            float? tilt = null,
            float? azimuth = null,
            string installDate = null,
            float? lossFactor = null,
            List<string> outputParameters = null,
            bool? terrainShading = null,
            string format = null
        )
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("latitude", latitude.ToString());
            parameters.Add("longitude", longitude.ToString());
            parameters.Add("capacity", capacity.ToString());
            if (timeZone != null) parameters.Add("timeZone", timeZone.ToString());
            if (period != null) parameters.Add("period", period.ToString());
            if (ghiWeight.HasValue) parameters.Add("ghiWeight", ghiWeight.Value.ToString());
            if (dniWeight.HasValue) parameters.Add("dniWeight", dniWeight.Value.ToString());
            if (probability != null) parameters.Add("probability", probability.ToString());
            if (tilt.HasValue) parameters.Add("tilt", tilt.Value.ToString());
            if (azimuth.HasValue) parameters.Add("azimuth", azimuth.Value.ToString());
            if (installDate != null) parameters.Add("installDate", installDate.ToString());
            if (lossFactor.HasValue) parameters.Add("lossFactor", lossFactor.Value.ToString());
            if (outputParameters != null && outputParameters.Any()) parameters.Add("outputParameters", string.Join(",", outputParameters));
            if (terrainShading.HasValue) parameters.Add("terrainShading", terrainShading.Value.ToString());
            if (format != null) parameters.Add("format", format.ToString());

            var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
            var response = await _httpClient.GetAsync(SolcastUrls.TmyRooftopPvPower + $"?{queryString}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedApiKeyException("The API key provided is invalid or unauthorized.");
            }

            response.EnsureSuccessStatusCode();

            var rawContent = await response.Content.ReadAsStringAsync();

            if (parameters.ContainsKey("format") && parameters["format"] == "json")
            {
                var data = JsonConvert.DeserializeObject<TmyRooftopPvPowerResponse>(rawContent);
                return new ApiResponse<TmyRooftopPvPowerResponse>(data, rawContent);
            }
            return new ApiResponse<TmyRooftopPvPowerResponse>(null, rawContent);
        }
    }
}
