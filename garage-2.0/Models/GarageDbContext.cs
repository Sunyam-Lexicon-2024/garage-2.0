﻿using garage_2._0.Enums;
using Microsoft.EntityFrameworkCore;

namespace garage_2._0.Models
{
    public class GarageDbContext : DbContext
    {
        public GarageDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ParkedVehicle> ParkedVehicles { get; set; }
        public DbSet<Garage> Garages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Garage>().HasData(new Garage
            {
                ID = 1,
                Name = "Default Garage One",
                MaxCapacity = 50,
            });

            var vehicles = GetParkedVehiclesData();
            modelBuilder.Entity<ParkedVehicle>().HasData(vehicles);
        }

        private List<ParkedVehicle> GetParkedVehiclesData()
        {
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

            return vehicles;
        }
    }
}
