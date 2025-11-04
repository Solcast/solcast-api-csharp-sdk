using Newtonsoft.Json;

namespace Solcast.Models
{
    public class PremiumPowerForecastSeries
    {
        [JsonProperty("data_series_id")]
        public string DataSeriesId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("measurement_type")]
        public string MeasurementType { get; set; }

        [JsonProperty("measurement_unit")]
        public string MeasurementUnit { get; set; }

        [JsonProperty("issue_date_time")]
        public string IssueDateTime { get; set; }
    }
}
