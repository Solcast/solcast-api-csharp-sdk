
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
    public class AggregationClient : BaseClient
    {
        public AggregationClient()
        {
        }
        
        /// <summary>
        /// Get live aggregation data for up to 7 days of data at a time for a requested collection or aggregation.
        /// </summary>
        /// <param name="outputParameters">The output parameters to include in the response.</param>
        /// <param name="collectionId">Unique identifier for your collection.</param>
        /// <param name="aggregationId">Unique identifier that belongs to the requested collection.</param>
        /// <param name="hours">The number of hours to return in the response.</param>
        /// <param name="period">Length of the averaging period in ISO 8601 format.</param>
        /// <param name="format">Response format</param>
        public async Task<ApiResponse<LiveAggregationResponse>> GetLiveAggregations(
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
                var response = await _httpClient.GetAsync(SolcastUrls.LiveAggregations + $"?{queryString}");

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
                    var data = JsonConvert.DeserializeObject<LiveAggregationResponse>(rawContent);
                    return new ApiResponse<LiveAggregationResponse>(data, rawContent);
                }
                return new ApiResponse<LiveAggregationResponse>(null, rawContent);
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
                throw new Exception($@"HTTP error in GetLiveAggregations
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "outputParameters=" + outputParameters + ", " + "collectionId=" + collectionId + ", " + "aggregationId=" + aggregationId + ", " + "hours=" + hours + ", " + "period=" + period + ", " + "format=" + format;
                throw new Exception($@"Unhandled error in GetLiveAggregations
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }        
        /// <summary>
        /// Get forecast aggregation data for up to 7 days of data at a time for a requested collection or aggregation.
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
        }    }
}
