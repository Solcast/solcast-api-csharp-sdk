using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class EstimateKimberLossFractionResponse
    {
        [JsonProperty("loss_fraction")]
        public List<double?> LossFraction { get; set; }

        [JsonProperty("response_status")]
        public string ResponseStatus { get; set; }
    }
}
