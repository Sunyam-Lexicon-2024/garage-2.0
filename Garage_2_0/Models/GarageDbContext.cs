﻿using Garage_2_0.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Models
{
    public class GarageDbContext : DbContext
    {
        public GarageDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Garage> Garages { get; set; }
        public DbSet<ParkedVehicle> ParkedVehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GarageConfiguration());
            modelBuilder.ApplyConfiguration(new ParkedVehicleConfiguration());
        }
    }
}
