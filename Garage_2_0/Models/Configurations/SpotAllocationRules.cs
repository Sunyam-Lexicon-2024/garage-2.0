using Garage_2_0.Models.Enums;
namespace Garage_2_0.Models.Configurations;

public class SpotAllocationRules
{
    public Dictionary<VehicleType, AllocationRuleItem> Collection { get; set; }

    public SpotAllocationRules()
    {
        // move out to appsettings?
        Collection = new Dictionary<VehicleType, AllocationRuleItem> {
            { VehicleType.Car, new() { MaxOccupied = 0, RequiredSpotCount = 1 }},
            { VehicleType.Motorcycle, new() { MaxOccupied = 2, RequiredSpotCount = 1 }},
            { VehicleType.Bus, new() { MaxOccupied = 0, RequiredSpotCount = 2 }},
            { VehicleType.Boat, new() { MaxOccupied = 0, RequiredSpotCount = 3 }},
            { VehicleType.Airplane, new() { MaxOccupied = 0, RequiredSpotCount = 4 }},
        };
    }

    public class AllocationRuleItem
    {
        public int MaxOccupied { get; set; }
        public int RequiredSpotCount { get; set; }
    }
}