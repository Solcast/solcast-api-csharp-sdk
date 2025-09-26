using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class HistoricForecastPvPowerResponse
    {
        [JsonProperty("historic_forecasts")]
        public List<Dictionary<string, object>> HistoricForecasts { get; set; }

        [JsonProperty("override_format_version")]
        public string OverrideFormatVersion { get; set; }
    }
}
