using Garage_2_0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Garage_2_0.Data.EntityConfigurations
{
    public class GarageConfiguration : IEntityTypeConfiguration<Garage>
    {
        public void Configure(EntityTypeBuilder<Garage> builder)
        {
            builder.ToTable("Garage");

            var garages = new List<Garage>
            {
                new Garage
                {
                    Id = 1,
                    Name = "Garage One",
                    MaxCapacity = 50
                },
                new Garage
                {
                    Id = 2,
                    Name = "Garage Two",
                    MaxCapacity = 100
                },
                new Garage
                {
                    Id = 3,
                    Name = "Garage Three",
                    MaxCapacity = 25
                },
            };


            builder.HasData(garages);
        }
    }
}
