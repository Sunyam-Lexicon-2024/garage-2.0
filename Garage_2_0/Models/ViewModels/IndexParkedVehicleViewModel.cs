namespace Garage_2_0.Models.ViewModels
{
    public class IndexParkedVehicleViewModel
    {
        public AlertViewModel? Alert { get; set; }
        public IEnumerable<ParkedVehicleSlimViewModel> ParkedVehicles { get; set; }

        public IndexParkedVehicleViewModel()
        {
            ParkedVehicles = new List<ParkedVehicleSlimViewModel>();
        }
    }
}