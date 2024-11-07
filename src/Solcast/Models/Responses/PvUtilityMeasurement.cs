using Newtonsoft.Json;

public class PvUtilityMeasurement
{
    [JsonProperty("id")]
    public int? Id { get; set; } 
    [JsonProperty("solar_farm_id")]
    public int? SolarFarmId { get; set; } 
    [JsonProperty("period_end")]
    public string PeriodEnd { get; set; } 
    [JsonProperty("period")]
    public string Period { get; set; } 
    [JsonProperty("created")]
    public string Created { get; set; } 
    [JsonProperty("total_power")]
    public double? TotalPower { get; set; } 
    [JsonProperty("inverters_available")]
    public int? InvertersAvailable { get; set; } 
    [JsonProperty("availability")]
    public double? Availability { get; set; } 
    [JsonProperty("mean_ghi")]
    public int? MeanGhi { get; set; } 
    [JsonProperty("tracking_angle")]
    public int? TrackingAngle { get; set; } 
    [JsonProperty("network_constraint")]
    public bool? NetworkConstraint { get; set; } 
    [JsonProperty("constraint_power_ceiling")]
    public double? ConstraintPowerCeiling { get; set; } 
    [JsonProperty("local_limit")]
    public double? LocalLimit { get; set; } 
    [JsonProperty("frequency")]
    public double? Frequency { get; set; } 
}
