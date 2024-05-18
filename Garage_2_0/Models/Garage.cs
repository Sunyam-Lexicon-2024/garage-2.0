namespace Garage_2_0.Models
{
    public class Garage
    {
        public Garage() => ParkingSpots = [];
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int ParkingSpotCount { get; set; }
        public ICollection<ParkingSpot> ParkingSpots { get; } = [];
    }
}
