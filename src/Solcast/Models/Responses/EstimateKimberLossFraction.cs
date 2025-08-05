using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class EstimateKimberLossFraction
    {
        [JsonProperty("rainfall")]
        public List<double?> Rainfall { get; set; }

        [JsonProperty("dates")]
        public List<string> Dates { get; set; }

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
