using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solcast.Models
{
    public class GetLiveHsuLosses
    {
        /// <summary>
        /// The latitude of the location you request data for. Must be a decimal number between -90 and 90.
        /// </summary>
        [JsonProperty("latitude")]
        public double? Latitude { get; set; } // Required

        /// <summary>
        /// The longitude of the location you request data for. Must be a decimal number between -180 and 180.
        /// </summary>
        [JsonProperty("longitude")]
        public double? Longitude { get; set; } // Required

        /// <summary>
        /// Timezone to return in data set. Accepted values are utc, longitudinal, or a range from -13 to 13 in 0.25 hour increments for utc offset.
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// The number of hours to return in the response.
        /// </summary>
        [JsonProperty("hours")]
        public int? Hours { get; set; }

        /// <summary>
        /// Length of the averaging period in ISO 8601 format.
        /// </summary>
        [JsonProperty("period")]
        public string Period { get; set; }

        /// <summary>
        /// Deposition Velocity for PM10.
        /// </summary>
        [JsonProperty("depo_veloc_pm10")]
        public double? DepoVelocPm10 { get; set; }

        /// <summary>
        /// Deposition Velocity for PM2.5.
        /// </summary>
        [JsonProperty("depo_veloc_pm2.5")]
        public double? DepoVelocPm2.5 { get; set; }

        /// <summary>
        /// Restricted. A list of PM10 values. Concentration of airborne particulate matter (PM) with aerodynamicdiameter less than 10 microns. [g/m^3] HSU endpoint will internally use Solcast's PM10 values tailored to your request time period.
        /// </summary>
        [JsonProperty("pm10")]
        public List<double?> Pm10 { get; set; }

        /// <summary>
        /// Restricted. A list of PM2.5 values. Concentration of airborne particulate matter (PM) with aerodynamicdiameter less than 10 microns. [g/m^3] HSU endpoint will internally use Solcast's PM2.5 values tailored to your request time period.
        /// </summary>
        [JsonProperty("pm2.5")]
        public List<double?> Pm2.5 { get; set; }

        /// <summary>
        /// The angle (degrees) that the PV system is tilted off the horizontal. A tilt of 0 means the system faces directly upwards, and 90 means the system is vertical and facing the horizon. If you don't specify tilt, we use a default tilt angle based on the latitude you specify in your request. Must be between 0 and 90.
        /// </summary>
        [JsonProperty("tilt")]
        public double? Tilt { get; set; }

        /// <summary>
        /// Amount of daily rainfall required to clean the panels (mm)
        /// </summary>
        [JsonProperty("cleaning_threshold")]
        public double? CleaningThreshold { get; set; }

        /// <summary>
        /// Initial percentage of energy lost due to soiling at time zero in the rainfall series input. If not provided, Solcast will perform a ramp up series calculation to accurately determine this value.
        /// </summary>
        [JsonProperty("initial_soiling")]
        public double? InitialSoiling { get; set; }

        /// <summary>
        /// A list of ISO 8601 compliant dates or a repeating interval when manual cleaning of the panels occurred.
        /// </summary>
        [JsonProperty("manual_wash_dates")]
        public List<string> ManualWashDates { get; set; }

        /// <summary>
        /// Restricted. Specifies the period of time to sum rain fall data. Internally this value is set to 24 hours in line with the findings of https://ieeexplore.ieee.org/document/4060159 .
        /// </summary>
        [JsonProperty("rain_accum_period")]
        public int? RainAccumPeriod { get; set; }

        /// <summary>
        /// Restricted. List of rainfall values. HSU endpoint will internally use Solcast's percepitation rate values tailored to your request time period.
        /// </summary>
        [JsonProperty("rainfall")]
        public List<double?> Rainfall { get; set; }

        /// <summary>
        /// Response format
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
