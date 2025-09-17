using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class ForecastsDataResponse
    {
        [JsonProperty("forecasts")]
        public List<Dictionary<string, object>> Forecasts { get; set; }
    }
}
