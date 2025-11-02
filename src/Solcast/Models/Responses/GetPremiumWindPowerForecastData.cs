using Newtonsoft.Json;

namespace Solcast.Models
{
    public class GetPremiumWindPowerForecastData
    {
        /// <summary>
        /// Premium Power data series identifier.
        /// </summary>
        [JsonProperty("data_series_id")]
        public string DataSeriesId { get; set; } // Required

        /// <summary>
        /// Desired output format (e.g. json, csv).
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }

        /// <summary>
        /// API key used to authorize the request.
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
