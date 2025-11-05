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
        /// Desired output format (e.g. json, csv). Defaults to json when omitted.
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
