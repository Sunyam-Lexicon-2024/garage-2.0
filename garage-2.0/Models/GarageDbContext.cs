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
    }
}
