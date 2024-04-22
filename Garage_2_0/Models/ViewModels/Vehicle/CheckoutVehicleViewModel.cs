namespace Garage_2_0.Models.ViewModels.Vehicle
{
    public class CheckoutVehicleViewModel
    {
        public int HourlyRate { get; set; }
        public decimal TotalParkingCost { get; set; }
        public DateTime CheckoutAt { get; set; }
        public TimeSpan ParkingPeriod { get; set; }
        public ParkedVehicleSlimViewModel Vehicle { get; set; } = default!;
    }
}
