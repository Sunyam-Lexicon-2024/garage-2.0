using Garage_2_0.Models.Enums;

namespace Garage_2_0.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }
        public required string RegistrationNumber { get; set; }
        public required VehicleType Type { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required int Wheels { get; set; }
        public required DateTime RegisteredAt { get; set; }
        public Color Color { get; set; }

        public int GarageId { get; set; }
        public Garage Garage { get; set; } = null!;
    }
}
