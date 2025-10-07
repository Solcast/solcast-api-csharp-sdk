using Newtonsoft.Json;

namespace Solcast.Models
{
    public class GetLiveKimberLosses
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
        /// Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset.
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

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
        /// Amount of daily rainfall required to clean the panels (mm)
        /// </summary>
        [JsonProperty("cleaning_threshold")]
        public int? CleaningThreshold { get; set; }

        /// <summary>
        /// Percentage of energy lost due to one day of soiling.
        /// </summary>
        [JsonProperty("soiling_loss_rate_percent")]
        public double? SoilingLossRatePercent { get; set; }

        /// <summary>
        /// Number of days after a rainfall event when it’s assumed the ground is damp, and so it’s assumed there is no soiling.
        /// </summary>
        [JsonProperty("grace_period")]
        public int? GracePeriod { get; set; }

        /// <summary>
        /// Maximum percentage of energy lost due to soiling. Soiling will build up until this value.
        /// </summary>
        [JsonProperty("max_soiling_percent")]
        public double? MaxSoilingPercent { get; set; }

        /// <summary>
        /// Initial percentage of energy lost due to soiling at time zero in the rainfall series input.
        /// </summary>
        [JsonProperty("initial_soiling_percent")]
        public double? InitialSoilingPercent { get; set; }

        /// <summary>
        /// Response format
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
