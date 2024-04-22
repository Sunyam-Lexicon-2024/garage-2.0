using Garage_2_0.Models.Enums;
using System.ComponentModel;

namespace Garage_2_0.Models.ViewModels
{
    public class CreateParkedVehicleViewModel
    {
        public VehicleType Type { get; set; }
        [DisplayName("Registration number")]
        public required string RegistrationNumber { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required int Wheels { get; set; }
        public required DateTime RegisteredAt { get; set; }
        public Color Color { get; set; }
    }
}
