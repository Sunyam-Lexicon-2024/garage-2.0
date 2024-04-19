using Garage_2_0.Models;
using Garage_2_0.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Garage_2_0.Data.EntityConfigurations
{
    public class ParkedVehicleConfiguration : IEntityTypeConfiguration<ParkedVehicle>
    {
        public void Configure(EntityTypeBuilder<ParkedVehicle> builder)
        {
            builder.ToTable("ParkedVehicle");

            var vehicles = new List<ParkedVehicle>
            {
                new ParkedVehicle
                {
                    Id = 1,
                    GarageId = 1,
                    RegistrationNumber = "FPD941",
                    Type = VehicleType.Car,
                    Brand = "Volkswagen",
                    Model = "Unknown",
                    Wheels = 4,
                    RegisteredAt = DateTime.Now,
                    Color = Color.Purple
                },
                new ParkedVehicle
                {
                    Id = 2,
                    GarageId = 1,
                    RegistrationNumber = "CLQ415",
                    Type = VehicleType.Car,
                    Brand = "Saab",
                    Model = "Unknown",
                    Wheels = 4,
                    RegisteredAt = DateTime.Now,
                    Color = Color.Yellow
                },
                new ParkedVehicle
                {
                    Id = 3,
                    GarageId = 1,
                    RegistrationNumber = "YHV901",
                    Type = VehicleType.Car,
                    Brand = "Volvo",
                    Model = "Unknown",
                    Wheels = 4,
                    RegisteredAt = DateTime.Now,
                    Color = Color.Blue
                },
                new ParkedVehicle
                {
                    Id = 4,
                    GarageId = 1,
                    RegistrationNumber = "GBO781",
                    Type = VehicleType.Car,
                    Brand = "Audi",
                    Model = "Unknown",
                    Wheels = 4,
                    RegisteredAt = DateTime.Now,
                    Color = Color.Black
                },
                new ParkedVehicle
                {
                    Id = 5,
                    GarageId = 1,
                    RegistrationNumber = "JRC132",
                    Type = VehicleType.Car,
                    Brand = "Toyota",
                    Model = "Unknown",
                    Wheels = 4,
                    RegisteredAt = DateTime.Now,
                    Color = Color.Green
                },
            };

            builder.HasData(vehicles);
        }
    }
}
