using Newtonsoft.Json;

namespace Solcast.Models
{
    public class TerrainElevationResponse
    {
        [JsonProperty("terrain_elevation")]
        public string TerrainElevation { get; set; }

        [JsonProperty("response_status")]
        public string ResponseStatus { get; set; }
    }
}
