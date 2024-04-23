using System.ComponentModel;

namespace Garage_2_0.Models.ViewModels
{
    public class GarageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [DisplayName("Parking Spot Count")]
        public int ParkingSpotCount { get; set; }
    }
}
