using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class EstimatedActualsDataResponse
    {
        [JsonProperty("estimated_actuals")]
        public List<Dictionary<string, object>> EstimatedActuals { get; set; }

        [JsonProperty("period_end_timestamp_format_json")]
        public string PeriodEndTimestampFormatJson { get; set; }

        [JsonProperty("period_end_timestamp_format_csv")]
        public string PeriodEndTimestampFormatCsv { get; set; }
    }
}
