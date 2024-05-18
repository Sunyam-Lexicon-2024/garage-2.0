using System.ComponentModel.DataAnnotations;

namespace Garage_2_0.Models.ViewModels.Garage
{
    public class CreateOrEditGarageViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(0, 500)]
        public int ParkingSpotCount { get; set; }
    }
}
