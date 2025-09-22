using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class GetHistoricKimberLosses
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
        /// Length of the averaging period in ISO 8601 format.
        /// </summary>
        [JsonProperty("period")]
        public string Period { get; set; }

        /// <summary>
        /// ISO_8601 compliant starting datetime for the historical data. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed.
        /// </summary>
        [JsonProperty("start")]
        public string Start { get; set; } // Required

        /// <summary>
        /// Must include one of end_date and duration. ISO_8601 compliant duration for the historical data. Must be within 31 days of the start_date.
        /// </summary>
        [JsonProperty("duration")]
        public string Duration { get; set; }

        /// <summary>
        /// Must include one of end_date and duration. ISO_8601 compliant ending datetime for the historical data. Must be within 31 days of the start_date. If the supplied value does not specify a timezone, the timezone will be inferred from the time_zone parameter, if supplied. Otherwise UTC is assumed.
        /// </summary>
        [JsonProperty("end")]
        public string End { get; set; }

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
        /// A list of ISO 8601 compliant dates or a repeating interval when manual cleaning of the panels occurred.
        /// </summary>
        [JsonProperty("manual_wash_dates")]
        public List<string> ManualWashDates { get; set; }

        /// <summary>
        /// Response format
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
