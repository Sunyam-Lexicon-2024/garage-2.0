using garage_2._0.Models.Enums;

namespace garage_2._0.ViewModels
{
    public class ParkedVehicleViewModel
    {
        public int Id { get; set; }
        public string? RegistrationNumber { get; set; }
        public VehicleType Type { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Wheels { get; set; }
        public DateTime RegisteredAt { get; set; }
        public Color Color { get; set; }
    }
}
