using Garage_2_0.Models.Enums;

namespace Garage_2_0.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int GarageId { get; set; }
        public required string RegistrationNumber { get; set; }
        public required VehicleType Type { get; set; }
        public required string Brand { get; set; } = "Unknown";
        public required string Model { get; set; } = "Unknown";
        public required int Wheels { get; set; }
        public DateTime RegisteredAt { get; } = DateTime.Now;
        public Color Color { get; set; }
        public ICollection<ParkingSpot> ParkingSpots { get; } = [];
    }
}
