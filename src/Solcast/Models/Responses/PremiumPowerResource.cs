using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class PremiumPowerResource
    {
        [JsonProperty("resource_id")]
        public string ResourceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("capacity")]
        public double? Capacity { get; set; }

        [JsonProperty("capacity_dc")]
        public double? CapacityDc { get; set; }

        [JsonProperty("azimuth")]
        public double? Azimuth { get; set; }

        [JsonProperty("tilt")]
        public double? Tilt { get; set; }

        [JsonProperty("tracking_type")]
        public string TrackingType { get; set; }

        [JsonProperty("timezone_db_id")]
        public string TimezoneDbId { get; set; }

        [JsonProperty("install_date")]
        public string InstallDate { get; set; }

        [JsonProperty("grid_export_limit")]
        public double? GridExportLimit { get; set; }

        [JsonProperty("module_type")]
        public string ModuleType { get; set; }

        [JsonProperty("ground_coverage_ratio")]
        public double? GroundCoverageRatio { get; set; }

        [JsonProperty("derating_temp_module")]
        public double? DeratingTempModule { get; set; }

        [JsonProperty("derating_age_system")]
        public double? DeratingAgeSystem { get; set; }

        [JsonProperty("derating_other_system")]
        public double? DeratingOtherSystem { get; set; }

        [JsonProperty("inverter_peak_efficiency")]
        public double? InverterPeakEfficiency { get; set; }

        [JsonProperty("inverter_efficiency_curve_type")]
        public string InverterEfficiencyCurveType { get; set; }

        [JsonProperty("tracker_axis_azimuth")]
        public double? TrackerAxisAzimuth { get; set; }

        [JsonProperty("tracker_max_rotation_angle")]
        public double? TrackerMaxRotationAngle { get; set; }

        [JsonProperty("tracker_back_tracking")]
        public bool? TrackerBackTracking { get; set; }

        [JsonProperty("tracker_smart_tracking")]
        public bool? TrackerSmartTracking { get; set; }

        [JsonProperty("terrain_slope")]
        public double? TerrainSlope { get; set; }

        [JsonProperty("terrain_azimuth")]
        public double? TerrainAzimuth { get; set; }

        [JsonProperty("dust_soiling_average")]
        public List<double?> DustSoilingAverage { get; set; }

        [JsonProperty("bifacial_system")]
        public bool? BifacialSystem { get; set; }

        [JsonProperty("site_ground_albedo")]
        public double? SiteGroundAlbedo { get; set; }

        [JsonProperty("bifaciality_factor")]
        public double? BifacialityFactor { get; set; }

        [JsonProperty("pvrow_height")]
        public double? PvrowHeight { get; set; }

        [JsonProperty("pvrow_width")]
        public double? PvrowWidth { get; set; }

        [JsonProperty("clearsky_zenith_coefficients")]
        public List<ZenithCoefficients> ClearskyZenithCoefficients { get; set; }

        [JsonProperty("cloudy_zenith_coefficients")]
        public List<ZenithCoefficients> CloudyZenithCoefficients { get; set; }

        [JsonProperty("confirmed_metadata")]
        public string ConfirmedMetadata { get; set; }

        [JsonProperty("forecast_collections")]
        public List<PremiumPowerForecastCollection> ForecastCollections { get; set; }
    }
}
