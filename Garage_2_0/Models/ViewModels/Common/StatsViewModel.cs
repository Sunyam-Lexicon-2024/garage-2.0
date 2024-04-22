using Garage_2_0.Models.Enums;
using System.ComponentModel;

namespace Garage_2_0.Models.ViewModels.Common;

public class StatsViewModel
{
    [DisplayName("Total accumulated revenue (SEK)")]
    public long AccumulatedRevenue { get; set; }
    [DisplayName("Total wheel count in garages")]
    public int TotalWheelCount { get; set; }
    public IDictionary<VehicleType, int> VehicleCountList { get; set; } = null!;
}