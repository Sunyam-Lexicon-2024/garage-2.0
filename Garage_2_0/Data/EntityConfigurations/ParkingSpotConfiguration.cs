using Garage_2_0.Models;
using Garage_2_0.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Garage_2_0.Data.EntityConfigurations;

public class ParkingSpotConfiguration : IEntityTypeConfiguration<ParkingSpot>
{
    public void Configure(EntityTypeBuilder<ParkingSpot> entity)
    {
        entity.HasData(
            new List<ParkingSpot>{
                new() { Id = 1, GarageId = 1, ContainsVehicleType = VehicleType.Car},
                new() { Id = 2, GarageId = 1, ContainsVehicleType = VehicleType.Car},
                new() { Id = 3, GarageId = 1, ContainsVehicleType = VehicleType.Car},
                new() { Id = 4, GarageId = 1, ContainsVehicleType = VehicleType.Car},
                new() { Id = 5, GarageId = 1, ContainsVehicleType = VehicleType.Car},
                new() { Id = 6, GarageId = 2},
                new() { Id = 7, GarageId = 2},
                new() { Id = 8, GarageId = 2},
                new() { Id = 9, GarageId = 2},
                new() { Id = 10, GarageId = 2},
            });
    }
}