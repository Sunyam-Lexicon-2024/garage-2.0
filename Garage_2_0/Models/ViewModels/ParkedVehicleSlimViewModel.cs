using Garage_2_0.Models.Enums;
using System.ComponentModel;

namespace Garage_2_0.Models.ViewModels
{
    public class ParkedVehicleSlimViewModel
    {
        public int Id { get; set; }
        [DisplayName("Registration Number")]
        public string? RegistrationNumber { get; set; }
        public VehicleType Type { get; set; }
        public string? Brand { get; set; }
        [DisplayName("Parked At")]
        public DateTime RegisteredAt { get; set; }
        public Color Color { get; set; }
    }
}
