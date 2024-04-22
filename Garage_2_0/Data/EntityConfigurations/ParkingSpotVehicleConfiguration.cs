using Garage_2_0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Garage_2_0.Data.EntityConfigurations;

public class ParkingSpotVehicleConfiguration : IEntityTypeConfiguration<ParkingSpot>
{
    public void Configure(EntityTypeBuilder<ParkingSpot> entity)
    {
        entity
            .HasMany(p => p.Vehicles)
            .WithMany(v => v.ParkingSpots)
            .UsingEntity(
                pv => pv.HasData(
                    new { VehiclesId = 1, ParkingSpotsId = 1 },
                    new { VehiclesId = 2, ParkingSpotsId = 2 },
                    new { VehiclesId = 3, ParkingSpotsId = 3 },
                    new { VehiclesId = 4, ParkingSpotsId = 4 },
                    new { VehiclesId = 5, ParkingSpotsId = 5 }
                )
            );
    }
}