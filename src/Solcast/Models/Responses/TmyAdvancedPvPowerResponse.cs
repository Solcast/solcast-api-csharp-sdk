using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class TmyAdvancedPvPowerResponse
    {
        [JsonProperty("estimated_actuals")]
        public List<Dictionary<string, object>> EstimatedActuals { get; set; }

        [JsonProperty("override_format_version")]
        public string OverrideFormatVersion { get; set; }
    }
}
