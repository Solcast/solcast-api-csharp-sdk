using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class UpdatePvPowerResource
    {
        /// <summary>
        /// The unique identifier of the resource.
        /// </summary>
        [JsonProperty("resource_id")]
        public string ResourceId { get; set; } // Required

        /// <summary>
        /// Type of resource (e.g. standard, premium).
        /// </summary>
        [JsonProperty("resource_type")]
        public string ResourceType { get; set; }

        /// <summary>
        /// Forecast collections and associated data series metadata for this resource.
        /// </summary>
        [JsonProperty("forecast_collections")]
        public List<PvPowerForecastCollection> ForecastCollections { get; set; }

        [JsonProperty("confirmed_metadata")]
        public string ConfirmedMetadata { get; set; }

        /// <summary>
        /// The name of the resource.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } // Required

        /// <summary>
        /// The latitude of the resource. Must be a decimal number between -90 and 90.
        /// </summary>
        [JsonProperty("latitude")]
        public double? Latitude { get; set; } // Required

        /// <summary>
        /// The longitude of the resource. Must be a decimal number between -180 and 180.
        /// </summary>
        [JsonProperty("longitude")]
        public double? Longitude { get; set; } // Required

        /// <summary>
        /// Total inverter (nameplate) capacity in MW. This is the highest potential output of the system before any Site Export Limit is applied. It is used to model the conversion of DC power to AC by your inverters.
        /// </summary>
        [JsonProperty("capacity")]
        public double? Capacity { get; set; }

        /// <summary>
        /// Total module capacity in MW. Usually slightly higher than the AC capacity. It is used to model the generation of DC power by your modules.
        /// </summary>
        [JsonProperty("capacity_dc")]
        public double? CapacityDc { get; set; }

        /// <summary>
        /// The angle from true north the modules are facing. North is 0, South is &#177;180, Eastward facing is negative values. Westward facing is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules.
        /// </summary>
        [JsonProperty("azimuth")]
        public double? Azimuth { get; set; }

        /// <summary>
        /// The off-horizontal tilt angle of modules for a fixed-tilt site.
        /// </summary>
        [JsonProperty("tilt")]
        public double? Tilt { get; set; }

        /// <summary>
        /// The type of sun-tracking or geometry configuration of your site's modules. It is used to calculate the incident irradiance for your modules.
        /// </summary>
        [JsonProperty("tracking_type")]
        public string TrackingType { get; set; }

        /// <summary>
        /// The date when your site was installed. It is used to derate your module (DC) production gradually with age, at a rate dependent on your Module Type.
        /// </summary>
        [JsonProperty("install_date")]
        public string InstallDate { get; set; }

        /// <summary>
        /// The maximum power export limit in MW that is allowed by the site’s connection with the network operator. It is used to place a final cap on your AC power output. Only impacts your AC power if the grid export limit is set lower than the AC capacity.
        /// </summary>
        [JsonProperty("grid_export_limit")]
        public double? GridExportLimit { get; set; }

        /// <summary>
        /// The type of material or technology used in your site's PV modules. It is used to estimate your module temperature derating coefficient (unless you specify your own coefficient), and used to estimate your module age derating.
        /// </summary>
        [JsonProperty("module_type")]
        public string ModuleType { get; set; }

        /// <summary>
        /// The proportion of the site's ground area covered by modules. It is used to calculate the incident irradiance for your modules.
        /// </summary>
        [JsonProperty("ground_coverage_ratio")]
        public double? GroundCoverageRatio { get; set; }

        /// <summary>
        /// The factor by which your site’s module (DC) production varies with differences in temperature from standard operating conditions ( 25 oC ). Can be found on module spec sheets and its often referred as: temperature coefficient of power or pmp.
        /// </summary>
        [JsonProperty("derating_temp_module")]
        public double? DeratingTempModule { get; set; }

        /// <summary>
        /// The factor by which the whole system will be derated per year since Install Date. It is used to calculate time dependent system loss.
        /// </summary>
        [JsonProperty("derating_age_system")]
        public double? DeratingAgeSystem { get; set; }

        /// <summary>
        /// The total system losses as a fraction of the total energy output. This excludes shading, soiling, snow, inverter, temperature and age. Includes losses such as wiring.
        /// </summary>
        [JsonProperty("derating_other_system")]
        public double? DeratingOtherSystem { get; set; }

        /// <summary>
        /// The peak efficiency value in your inverter efficiency curve. It is used to scale the conversion efficiency of DC to AC, as a function of the inverter load.
        /// </summary>
        [JsonProperty("inverter_peak_efficiency")]
        public double? InverterPeakEfficiency { get; set; }

        /// <summary>
        /// The off north-south azimuth angle for a horizontal single axis tracking site. Most commonly this will be close to zero.  North is 0, South is &#177;180, Eastward facing is negative values. Westward facing is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules.
        /// </summary>
        [JsonProperty("tracker_axis_azimuth")]
        public double? TrackerAxisAzimuth { get; set; }

        /// <summary>
        /// The maximum off-horizontal angle for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules.
        /// </summary>
        [JsonProperty("tracker_max_rotation_angle")]
        public double? TrackerMaxRotationAngle { get; set; }

        /// <summary>
        /// Whether the trackers backtrack at low solar elevation angles, for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules.
        /// </summary>
        [JsonProperty("tracker_back_tracking")]
        public bool? TrackerBackTracking { get; set; }

        /// <summary>
        /// Whether the trackers move to horizontal during cloudy periods with zero DNI, for a horizontal single axis tracking site. It is used to calculate the incident irradiance for your modules.
        /// </summary>
        [JsonProperty("tracker_smart_tracking")]
        public bool? TrackerSmartTracking { get; set; }

        /// <summary>
        /// The average terrain slope in degrees from horizontal of your site. A site with no terrain slope has a value of zero.
        /// </summary>
        [JsonProperty("terrain_slope")]
        public double? TerrainSlope { get; set; }

        /// <summary>
        /// The average terrain slope downhill direction. North is 0, South is &#177;180, Eastward is negative values. Westward is positive values. For example, -90 is due east. It is used to calculate the incident irradiance for your modules.
        /// </summary>
        [JsonProperty("terrain_azimuth")]
        public double? TerrainAzimuth { get; set; }

        /// <summary>
        /// The average proportion of module production lost due to dust soiling. The value entered should reflect the impact of cleaning activity at your site. It is used to calculate the module (DC) production.
        /// </summary>
        [JsonProperty("dust_soiling_average")]
        public List<double?> DustSoilingAverage { get; set; }

        /// <summary>
        /// Bifacial systems have modules that produce solar power not only from the front, but also the rear side of the panel. Used to create additional parameters that will allow us to model your production more accurately.
        /// </summary>
        [JsonProperty("bifacial_system")]
        public bool? BifacialSystem { get; set; }

        /// <summary>
        /// The proportion of the incident downward irradiance reflected by the ground surface in the area underneath the PV modules. Used to calculate local ground-reflected irradiance. Particularly important for bifacial systems.
        /// </summary>
        [JsonProperty("site_ground_albedo")]
        public double? SiteGroundAlbedo { get; set; }

        /// <summary>
        /// For bifacial modules, the module rear efficiency as a proportion of the front efficiency subject to the same irradiance. Used to calculate the module rear production for bifacial systems.
        /// </summary>
        [JsonProperty("bifaciality_factor")]
        public double? BifacialityFactor { get; set; }

        /// <summary>
        /// The height of the module rows, in metres, measured from the ground to the row center or axis. Used to calculate the module rear incident irradiance and production for bifacial systems.
        /// </summary>
        [JsonProperty("pvrow_height")]
        public double? PvrowHeight { get; set; }

        /// <summary>
        /// The width of the module rows, in metres. This is the cross-section width of the entire PV row. Used to calculate the module rear incident irradiance and production for bifacial systems.
        /// </summary>
        [JsonProperty("pvrow_width")]
        public double? PvrowWidth { get; set; }

        [JsonProperty("clearsky_zenith_coefficients")]
        public List<ZenithCoefficients> ClearskyZenithCoefficients { get; set; }

        [JsonProperty("cloudy_zenith_coefficients")]
        public List<ZenithCoefficients> CloudyZenithCoefficients { get; set; }
    }
}
