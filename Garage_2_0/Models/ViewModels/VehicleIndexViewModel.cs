using Garage_2_0.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage_2_0.Models.ViewModels
{
    public class VehicleIndexViewModel
    {
        public AlertViewModel? Alert { get; set; }
        public VehicleType? SelectedVehicleType { get; set; }
        public string? RegNumber { get; set; }

        public SelectList VehicleTypes = new(Enum.GetValues(typeof(VehicleType)));
        public IEnumerable<VehicleSlimViewModel> ParkedVehicles { get; set; } = new List<VehicleSlimViewModel>();
    }
}