using Newtonsoft.Json;

namespace Solcast.Models
{
    public class GetPvPowerResource
    {
        /// <summary>
        /// The unique identifier of the resource.
        /// </summary>
        [JsonProperty("resource_id")]
        public string ResourceId { get; set; } // Required

        /// <summary>
        /// When true, forecast collections and series metadata are included in the response.
        /// </summary>
        [JsonProperty("include_forecast_collections")]
        public bool? IncludeForecastCollections { get; set; }

        /// <summary>
        /// Type of resource (e.g. standard, premium).
        /// </summary>
        [JsonProperty("resource_type")]
        public string ResourceType { get; set; }

        /// <summary>
        /// Desired output format (e.g. json, csv).
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }

        /// <summary>
        /// API key used to authorise the request.
        /// </summary>
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        /// <summary>
        /// Custom JSON configuration applied to the response.
        /// </summary>
        [JsonProperty("jsconfig")]
        public string Jsconfig { get; set; }
    }
}
