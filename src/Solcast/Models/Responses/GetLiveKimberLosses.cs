using System.Collections.Generic;
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
        /// Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset. Default is utc.
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// The number of hours to return in the response. Default is 24.
        /// </summary>
        [JsonProperty("hours")]
        public int? Hours { get; set; }

        /// <summary>
        /// Length of the averaging period in ISO 8601 format. Default is PT30M.
        /// </summary>
        [JsonProperty("period")]
        public string Period { get; set; }

        /// <summary>
        /// Amount of daily rainfall required to clean the panels (mm). Default is 6.0.
        /// </summary>
        [JsonProperty("cleaning_threshold")]
        public double? CleaningThreshold { get; set; }

        /// <summary>
        /// Percentage of energy lost due to one day of soiling.
        /// </summary>
        [JsonProperty("soiling_loss_rate")]
        public double? SoilingLossRate { get; set; }

        /// <summary>
        /// Number of days after a rainfall event when it’s assumed the ground is damp, and so it’s assumed there is no soiling.
        /// </summary>
        [JsonProperty("grace_period")]
        public int? GracePeriod { get; set; }

        /// <summary>
        /// Maximum percentage of energy lost due to soiling. Soiling will build up until this value.
        /// </summary>
        [JsonProperty("max_soiling")]
        public double? MaxSoiling { get; set; }

        /// <summary>
        /// Initial percentage of energy lost due to soiling at time zero in the rainfall series input. If not provided, Solcast will perform a ramp up series calculation to accurately determine this value.
        /// </summary>
        [JsonProperty("initial_soiling")]
        public double? InitialSoiling { get; set; }

        /// <summary>
        /// A list of ISO_8601 compliant dates or a repeating interval when manual cleaning of the panels occurred. A list of dates example: [2025-01-01,2025-01-05,2025-01-10]. A repeating interval example: R3/2025-01-01T00:00:00Z/P14D. Wash dates outside of the start and end of the request are discarded.
        /// </summary>
        [JsonProperty("manual_wash_dates")]
        public List<string> ManualWashDates { get; set; }

        /// <summary>
        /// Response format (json, csv). Default is json.
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
