using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class PremiumPowerForecastCollection
    {
        [JsonProperty("collection_id")]
        public string CollectionId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("forecasts")]
        public List<PremiumPowerForecastSeries> Forecasts { get; set; }
    }
}
