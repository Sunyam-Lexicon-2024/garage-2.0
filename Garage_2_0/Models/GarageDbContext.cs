using Garage_2_0.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Models
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
        public DbSet<Garage_2_0.ViewModels.GarageViewModel> GarageViewModel { get; set; } = default!;
    }
}
