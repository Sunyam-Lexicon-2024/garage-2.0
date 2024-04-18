using garage_2._0.Enums;

namespace garage_2._0.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }
        public int GarageId { get; set; }
        public Garage Garage { get; set; } = null!;
        public required string RegistrationNumber { get; set; }
        public required VehicleType Type { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required int Wheels { get; set; }
        public required DateTime RegisteredAt { get; set; }
        public Color Color { get; set; }
    }
}
