using garage_2._0.Data.EntityConfigurations;
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
            modelBuilder.ApplyConfiguration(new GarageConfiguration());
            modelBuilder.ApplyConfiguration(new ParkedVehicleConfiguration());
        }
    }
}
