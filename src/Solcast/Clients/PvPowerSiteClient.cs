
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
    public class PvPowerSiteClient : BaseClient
    {
        public PvPowerSiteClient()
        {
        }

        public async Task<ApiResponse<string>> GetPvPowerSites(

        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.PvPowerSites + $"?{queryString}");

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
                    var data = JsonConvert.DeserializeObject<string>(rawContent);
                    return new ApiResponse<string>(data, rawContent);
                }
                return new ApiResponse<string>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "no parameters";
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetPvPowerSites
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "no parameters";
                throw new Exception($@"Unhandled error in GetPvPowerSites
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <param name="resourceId">The unique identifier of the resource.</param>
        public async Task<ApiResponse<PvPowerResource>> GetPvPowerSite(
            string resourceId
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("resourceId", resourceId.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.GetAsync(SolcastUrls.PvPowerSite + $"?{queryString}");

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
                    var data = JsonConvert.DeserializeObject<PvPowerResource>(rawContent);
                    return new ApiResponse<PvPowerResource>(data, rawContent);
                }
                return new ApiResponse<PvPowerResource>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "resourceId=" + resourceId;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in GetPvPowerSite
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "resourceId=" + resourceId;
                throw new Exception($@"Unhandled error in GetPvPowerSite
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <param name="body"></param>
        public async Task<ApiResponse<PvPowerResource>> PostPvPowerSite(
            CreatePvPowerResource body
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();

                var jsonContent = JsonConvert.SerializeObject(body);
                var requestBody = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
        
                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.PostAsync(SolcastUrls.PvPowerSite + $"?{queryString}", requestBody);

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
                    var data = JsonConvert.DeserializeObject<PvPowerResource>(rawContent);
                    return new ApiResponse<PvPowerResource>(data, rawContent);
                }
                return new ApiResponse<PvPowerResource>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "body=" + body;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in PostPvPowerSite
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "body=" + body;
                throw new Exception($@"Unhandled error in PostPvPowerSite
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <param name="body"></param>
        public async Task<ApiResponse<PvPowerResource>> PutPvPowerSite(
            UpdatePvPowerResource body
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();

                var jsonContent = JsonConvert.SerializeObject(body);
                var requestBody = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
        
                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.PutAsync(SolcastUrls.PvPowerSite + $"?{queryString}", requestBody);

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
                    var data = JsonConvert.DeserializeObject<PvPowerResource>(rawContent);
                    return new ApiResponse<PvPowerResource>(data, rawContent);
                }
                return new ApiResponse<PvPowerResource>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "body=" + body;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in PutPvPowerSite
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "body=" + body;
                throw new Exception($@"Unhandled error in PutPvPowerSite
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <param name="body"></param>
        public async Task<ApiResponse<PvPowerResource>> PatchPvPowerSite(
            PatchPvPowerResource body
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();

                var jsonContent = JsonConvert.SerializeObject(body);
                var requestBody = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
        
                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.PatchAsync(SolcastUrls.PvPowerSite + $"?{queryString}", requestBody);

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
                    var data = JsonConvert.DeserializeObject<PvPowerResource>(rawContent);
                    return new ApiResponse<PvPowerResource>(data, rawContent);
                }
                return new ApiResponse<PvPowerResource>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "body=" + body;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in PatchPvPowerSite
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "body=" + body;
                throw new Exception($@"Unhandled error in PatchPvPowerSite
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }
        /// <param name="resourceId">The unique identifier of the resource.</param>
        public async Task<ApiResponse<string>> DeletePvPowerSite(
            string resourceId
        )
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("resourceId", resourceId.ToString());

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? string.Empty)}"));
                var response = await _httpClient.DeleteAsync(SolcastUrls.PvPowerSite + $"?{queryString}");

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
                    var data = JsonConvert.DeserializeObject<string>(rawContent);
                    return new ApiResponse<string>(data, rawContent);
                }
                return new ApiResponse<string>(null, rawContent);
            }
            catch (UnauthorizedApiKeyException)
            {
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                var paramDetails = "resourceId=" + resourceId;
                var status = httpEx.StatusCode.HasValue ? ((int)httpEx.StatusCode).ToString() : "unknown";
                var content = httpEx.Data.Contains("Content") ? httpEx.Data["Content"] : "no content";
                throw new Exception($@"HTTP error in DeletePvPowerSite
Parameters: {paramDetails}
Status Code: {status}
Content: {content}
Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                var paramDetails = "resourceId=" + resourceId;
                throw new Exception($@"Unhandled error in DeletePvPowerSite
Parameters: {paramDetails}
Error: {ex.Message}", ex);
            }
        }    }
}
