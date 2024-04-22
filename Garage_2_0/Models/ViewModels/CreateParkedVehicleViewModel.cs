using Garage_2_0.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Garage_2_0.Models.ViewModels
{
    public class CreateParkedVehicleViewModel
    {
        public VehicleType Type { get; set; }
        [DisplayName("Registration number")]
        public string? RegistrationNumber { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Wheels { get; set; }
        public DateTime RegisteredAt { get; set; }
        public Color Color { get; set; }
        public IEnumerable<SelectListItem> Garages { get; set; } = new List<SelectListItem>();
    }
}
