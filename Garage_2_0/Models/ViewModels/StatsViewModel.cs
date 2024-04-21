using Garage_2_0.Models.Enums;

namespace Garage_2_0.Models.ViewModels;

public class StatsViewModel
{
    public int TotalWheelCount;
    public long AccumulatedRevenue;
    public IDictionary<VehicleType, int> VehicleCountList { get; set; } = null!;
}