using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class GetForecastRadiationAndWeather
    {
        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        /// <summary>
        /// The number of hours to return in the response.
        /// </summary>
        [JsonProperty("hours")]
        public int? Hours { get; set; }

        /// <summary>
        /// Length of the averaging period in ISO 8601 format.
        /// </summary>
        [JsonProperty("period")]
        public string Period { get; set; }

        /// <summary>
        /// The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90.
        /// </summary>
        [JsonProperty("tilt")]
        public float? Tilt { get; set; }

        /// <summary>
        /// The azimuth is defined as the angle (degrees) from true north that the PV system is facing. An azimuth of 0 means the system is facing true north. Positive values are anticlockwise, so azimuth is -90 for an east-facing system and 135 for a southwest-facing system. If you don't specify an azimuth, we use a default value of 0 (north facing) in the southern hemisphere and 180 (south-facing) in the northern hemisphere.
        /// </summary>
        [JsonProperty("azimuth")]
        public float? Azimuth { get; set; }

        /// <summary>
        /// The type of sun-tracking or geometry configuration of your site's modules.
        /// </summary>
        [JsonProperty("array_type")]
        public string ArrayType { get; set; }

        /// <summary>
        /// The output parameters to include in the response.
        /// </summary>
        [JsonProperty("output_parameters")]
        public List<string> OutputParameters { get; set; }

        /// <summary>
        /// If true, irradiance parameters are modified based on the surrounding terrain from a 90m-horizontal-resolution digital elevation model. The direct component of irradiance is set to zero when the beam from the sun is blocked by the terrain. The diffuse component of irradiance is reduced throughout the day if the sky view at the location is significantly reduced by the surrounding terrain. Global irradiance incorporates both effects.
        /// </summary>
        [JsonProperty("terrain_shading")]
        public bool? TerrainShading { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

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
        /// Response format
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
