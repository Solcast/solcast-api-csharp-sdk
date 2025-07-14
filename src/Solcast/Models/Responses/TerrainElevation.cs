using Newtonsoft.Json;

namespace Solcast.Models
{
    public class TerrainElevation
    {
        [JsonProperty("meters")]
        public double? Meters { get; set; }
    }
}
