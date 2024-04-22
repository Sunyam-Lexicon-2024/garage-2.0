using Garage_2_0.Models.Enums;

namespace Garage_2_0.Models;

public class ParkingSpot
{
    public ParkingSpot() => Vehicles = [];

    public int Id { get; set; }
    public int GarageId { get; set; }
    public VehicleType? ContainsVehicleType { get; set; } = null;
    public Garage Garage { get; set; } = null!;
    public ICollection<Vehicle> Vehicles { get; } = [];
}