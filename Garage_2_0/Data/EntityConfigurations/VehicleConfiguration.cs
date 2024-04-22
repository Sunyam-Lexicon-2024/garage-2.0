using Garage_2_0.Models;
using Garage_2_0.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Garage_2_0.Data.EntityConfigurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> entity)
    {
        entity.HasData(new List<Vehicle>
            {
                new()
                {
                    Id = 1,
                    GarageId = 1,
                    RegistrationNumber = "FPD941",
                    Type = VehicleType.Car,
                    Brand = "Volkswagen",
                    Model = "Unknown",
                    Wheels = 4,
                    Color = Color.Purple,
                },
                new()
                {
                    Id = 2,
                    GarageId = 1,
                    RegistrationNumber = "CLQ415",
                    Type = VehicleType.Car,
                    Brand = "Saab",
                    Model = "Unknown",
                    Wheels = 4,
                    Color = Color.Yellow,
                },
                new()
                {
                    Id = 3,
                    GarageId = 1,
                    RegistrationNumber = "YHV901",
                    Type = VehicleType.Car,
                    Brand = "Volvo",
                    Model = "Unknown",
                    Wheels = 4,
                    Color = Color.Blue,
                },
                new() {
                    Id = 4,
                    GarageId = 1,
                    RegistrationNumber = "GBO781",
                    Type = VehicleType.Car,
                    Brand = "Audi",
                    Model = "Unknown",
                    Wheels = 4,
                    Color = Color.Black,
                },
                new()
                {
                    Id = 5,
                    GarageId = 1,
                    RegistrationNumber = "JRC132",
                    Type = VehicleType.Car,
                    Brand = "Toyota",
                    Model = "Unknown",
                    Wheels = 4,
                    Color = Color.Green,
                },
            });

    }
}
