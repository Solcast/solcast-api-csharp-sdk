using Newtonsoft.Json;

namespace Solcast.Models
{
    public class PowerSiteResource
    {
        /// <summary>
        /// The unique identifier of the resource.
        /// </summary>
        [JsonProperty("resource_id")]
        public string ResourceId { get; set; } // Required

        /// <summary>
        /// The name of the resource.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } // Required

        /// <summary>
        /// The latitude of the resource. Must be a decimal number between -90 and 90.
        /// </summary>
        [JsonProperty("latitude")]
        public double? Latitude { get; set; } // Required

        /// <summary>
        /// The longitude of the resource. Must be a decimal number between -180 and 180.
        /// </summary>
        [JsonProperty("longitude")]
        public double? Longitude { get; set; } // Required

        /// <summary>
        /// Total inverter (nameplate) capacity in MW. This is the highest potential output of the system before any Site Export Limit is applied. It is used to model the conversion of DC power to AC by your inverters.
        /// </summary>
        [JsonProperty("capacity")]
        public double? Capacity { get; set; }

        /// <summary>
        /// The date when your site was installed. It is used to derate your module (DC) production gradually with age, at a rate dependent on your Module Type.
        /// </summary>
        [JsonProperty("install_date")]
        public string InstallDate { get; set; }
    }
}
