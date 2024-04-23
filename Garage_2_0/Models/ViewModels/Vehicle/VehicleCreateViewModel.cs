using Garage_2_0.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garage_2_0.Models.ViewModels.Vehicle
{
    public class VehicleCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        public VehicleType Type { get; set; }

        [DisplayName("Registration number")]
        [Required]
        public string? RegistrationNumber { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Model { get; set; }

        [Required]
        public int Wheels { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegisteredAt { get; set; }

        [Required]
        public Color Color { get; set; }

        [DisplayName("Garage")]
        [Required]
        public int GarageId { get; set; }
        public IEnumerable<SelectListItem> Garages { get; set; } = new List<SelectListItem>();
    }
}
