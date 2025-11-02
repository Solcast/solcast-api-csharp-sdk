using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class EstimatedActualsDataResponse
    {
        [JsonProperty("estimated_actuals")]
        public List<Dictionary<string, object>> EstimatedActuals { get; set; }
    }
}
