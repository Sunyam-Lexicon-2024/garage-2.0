using Garage_2_0.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Models
{
    public class GarageDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Garage> Garages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GarageConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        }
    }
}
