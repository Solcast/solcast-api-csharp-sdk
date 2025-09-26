using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class ForecastAggregationResponse
    {
        [JsonProperty("forecasts")]
        public List<Dictionary<string, object>> Forecasts { get; set; }

        [JsonProperty("override_format_version")]
        public string OverrideFormatVersion { get; set; }
    }
}
