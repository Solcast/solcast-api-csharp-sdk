using Newtonsoft.Json;

namespace Solcast.Models
{
    public class GetHorizonAngle
    {
        /// <summary>
        /// The latitude of the location you request data for. Must be a decimal number between -90 and 90.
        /// </summary>
        [JsonProperty("latitude")]
        public double? Latitude { get; set; } // Required

        /// <summary>
        /// The longitude of the location you request data for. Must be a decimal number between -180 and 180.
        /// </summary>
        [JsonProperty("longitude")]
        public double? Longitude { get; set; } // Required

        /// <summary>
        /// Number of sections to divide the azimuth circle into, governing how many items are returned in the response. E.g. 10 = [-180°, -144°, -108°, -72°, -36°, 0°, 36°, 72°, 108°, 144°] Maximum 360
        /// </summary>
        [JsonProperty("azimuth_intervals")]
        public int? AzimuthIntervals { get; set; } // Required

        /// <summary>
        /// Response format
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
