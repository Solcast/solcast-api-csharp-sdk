using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class PvPowerForecastCollection
    {
        /// <summary>
        /// Unique identifier for the forecast collection.
        /// </summary>
        [JsonProperty("collection_id")]
        public string CollectionId { get; set; }

        /// <summary>
        /// Collection display name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Forecast data series available within this collection.
        /// </summary>
        [JsonProperty("forecasts")]
        public List<PvPowerForecastSeries> Forecasts { get; set; }
    }
}
