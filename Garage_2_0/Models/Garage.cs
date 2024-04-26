namespace Garage_2_0.Models
{
    public class Garage
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int MaxCapacity { get; set; }
        public ICollection<ParkedVehicle> ParkedVehicles { get; } = [];
    }
}
