using Newtonsoft.Json;

namespace Solcast.Models
{
    public class ListPremiumPowerResources
    {
        /// <summary>
        /// When true, forecast collections and series metadata are included in responses.
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
