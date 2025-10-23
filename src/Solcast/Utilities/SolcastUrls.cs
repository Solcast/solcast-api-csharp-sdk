
namespace Solcast
{
    public static class SolcastUrls
    {
        public static readonly string BaseUrl = "https://api.solcast.com.au";


        // live data endpoints
        public static readonly string LiveAdvancedPvPower = "data/live/advanced_pv_power";
        public static readonly string LiveAggregations = "data/live/aggregations";
        public static readonly string LiveRooftopPvPower = "data/live/rooftop_pv_power";
        public static readonly string LiveRadiationAndWeather = "data/live/radiation_and_weather";

        // forecast data endpoints
        public static readonly string ForecastAdvancedPvPower = "data/forecast/advanced_pv_power";
        public static readonly string ForecastAggregations = "data/forecast/aggregations";
        public static readonly string ForecastRooftopPvPower = "data/forecast/rooftop_pv_power";
        public static readonly string ForecastRadiationAndWeather = "data/forecast/radiation_and_weather";

        // historic data endpoints
        public static readonly string HistoricAdvancedPvPower = "data/historic/advanced_pv_power";
        public static readonly string HistoricRadiationAndWeather = "data/historic/radiation_and_weather";
        public static readonly string HistoricRooftopPvPower = "data/historic/rooftop_pv_power";

        // tmy data endpoints
        public static readonly string TmyAdvancedPvPower = "data/tmy/advanced_pv_power";
        public static readonly string TmyRadiationAndWeather = "data/tmy/radiation_and_weather";
        public static readonly string TmyRooftopPvPower = "data/tmy/rooftop_pv_power";

        // pv power site data endpoints
        public static readonly string PvPowerSites = "resources/pv_power_sites";
        public static readonly string PvPowerSite = "resources/pv_power_site";

    }
}
