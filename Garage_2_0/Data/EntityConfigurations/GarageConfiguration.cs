using Garage_2_0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Garage_2_0.Data.EntityConfigurations
{
    public class GarageConfiguration : IEntityTypeConfiguration<Garage>
    {
        public void Configure(EntityTypeBuilder<Garage> builder)
        {

            Garage[] garages = [

            new()
            {
                Id = 1,
                Name = "Default Garage 1",
                ParkingSpotCount = 5
            },
            new()
            {
                Id = 2,
                Name = "Default Garage 2",
                ParkingSpotCount = 5
            },

            ];

            builder.HasData(garages);
        }
    }
}

