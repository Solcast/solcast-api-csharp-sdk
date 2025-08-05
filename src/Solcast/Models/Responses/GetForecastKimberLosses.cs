using Newtonsoft.Json;

namespace Solcast.Models
{
    public class GetForecastKimberLosses
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("hours")]
        public int? Hours { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("cleaning_threshold")]
        public int? CleaningThreshold { get; set; }

        [JsonProperty("soiling_loss_rate")]
        public double? SoilingLossRate { get; set; }

        [JsonProperty("grace_period")]
        public int? GracePeriod { get; set; }

        [JsonProperty("max_soiling")]
        public double? MaxSoiling { get; set; }

        [JsonProperty("initial_soiling")]
        public double? InitialSoiling { get; set; }

        [JsonProperty("rain_accum_period")]
        public int? RainAccumPeriod { get; set; }
    }
}
