using Garage_2_0.Models;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Repositories
{
    public class VehicleRepository(GarageDbContext context) : IRepository<Vehicle>
    {
        private readonly GarageDbContext _context = context;

        public async Task<IEnumerable<Vehicle>> All()
        {
            var vehicles = await _context.Vehicles.ToListAsync();
            return vehicles;
        }

        public async Task<bool> Any(Func<Vehicle, bool> predicate)
        {
            bool any = await Task.Run(() => { return _context.Vehicles.Any(predicate); });
            return any;
        }

        public async Task<IEnumerable<Vehicle>> Find(Func<Vehicle, bool> predicate)
        {
            var vehicles = await Task.Run(() =>
            {
                return _context.Vehicles.Where(predicate).ToList();
            });
            return vehicles;
        }

        public async Task<Vehicle> Create(Vehicle entity)
        {
            var createdVehicle = await _context.Vehicles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return createdVehicle.Entity;
        }

        public async Task<Vehicle?> Delete(int id)
        {
            var vehicleToDelete = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
            if (vehicleToDelete != null)
            {
                var deletedVehicle = _context.Vehicles.Remove(vehicleToDelete).Entity;
                await _context.SaveChangesAsync();
                return deletedVehicle;
            }
            return null;
        }

        public async Task<Vehicle> Update(Vehicle entity)
        {
            var updatedVehicle = _context.Vehicles.Update(entity).Entity;
            await _context.SaveChangesAsync();
            return updatedVehicle;
        }


    }
}
