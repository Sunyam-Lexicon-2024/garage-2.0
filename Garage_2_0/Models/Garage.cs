namespace Garage_2_0.Models
{
    public class Garage
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required int MaxCapacity { get; set; }
        public ICollection<Vehicle> ParkedVehicles { get; } = [];

    }
}
