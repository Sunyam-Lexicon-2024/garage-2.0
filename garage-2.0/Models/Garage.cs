﻿namespace garage_2._0.Models
{
    public class Garage
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required int MaxCapacity { get; set; }
        public ICollection<ParkedVehicle> ParkedVehicles { get; } = [];

    }
}