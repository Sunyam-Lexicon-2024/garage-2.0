using garage_2._0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace garage_2._0.Data.EntityConfigurations
{
    public class GarageConfiguration : IEntityTypeConfiguration<Garage>
    {
        public void Configure(EntityTypeBuilder<Garage> builder)
        {
            builder.ToTable("Garage");

            var garage = new Garage
            {
                ID = 1,
                Name = "Default Garage One",
                MaxCapacity = 50,
            };

            builder.HasData(garage);
        }
    }
}
