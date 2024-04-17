using garage_2._0.Enums;

namespace garage_2._0.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }
        public required string RegistrationNumber { get; set; }
        public VehicleType Type { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int Wheels { get; set; }
    }
}
