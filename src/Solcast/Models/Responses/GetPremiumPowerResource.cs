using Newtonsoft.Json;

namespace Solcast.Models
{
    public class GetPremiumPowerResource
    {
        /// <summary>
        /// The unique identifier of the premium resource.
        /// </summary>
        [JsonProperty("resource_id")]
        public string ResourceId { get; set; } // Required

        /// <summary>
        /// When true, forecast collections and series metadata are included in the response.
        /// </summary>
        [JsonProperty("include_forecast_collections")]
        public bool? IncludeForecastCollections { get; set; }

        /// <summary>
        /// Desired output format (e.g. json, csv). Defaults to json when omitted.
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
