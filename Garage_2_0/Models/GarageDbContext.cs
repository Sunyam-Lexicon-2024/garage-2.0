using Garage_2_0.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Models
{
    public class GarageDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().Property(v => v.RegisteredAt)
                                        .ValueGeneratedOnAdd()
                                        .HasDefaultValue(DateTime.Now);

            modelBuilder.ApplyConfiguration(new GarageConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new ParkingSpotConfiguration());
            modelBuilder.ApplyConfiguration(new ParkingSpotVehicleConfiguration());
        }
    }
}
