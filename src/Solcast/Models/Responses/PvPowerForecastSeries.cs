using Newtonsoft.Json;

namespace Solcast.Models
{
    public class PvPowerForecastSeries
    {
        /// <summary>
        /// Forecaster data series identifier.
        /// </summary>
        [JsonProperty("data_series_id")]
        public string DataSeriesId { get; set; }

        /// <summary>
        /// Forecast series display name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Measurement type exposed by the series.
        /// </summary>
        [JsonProperty("measurement_type")]
        public string MeasurementType { get; set; }

        /// <summary>
        /// Unit in which the series is measured.
        /// </summary>
        [JsonProperty("measurement_unit")]
        public string MeasurementUnit { get; set; }

        /// <summary>
        /// Issue timestamp associated with the forecast series metadata.
        /// </summary>
        [JsonProperty("issue_date_time")]
        public string IssueDateTime { get; set; }
    }
}
