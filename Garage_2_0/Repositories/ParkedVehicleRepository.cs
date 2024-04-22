using Garage_2_0.Models;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Repositories
{
    public class ParkedVehicleRepository(GarageDbContext context) : IRepository<ParkedVehicle>
    {
        private readonly GarageDbContext _context = context;

        public async Task<IEnumerable<ParkedVehicle>> All()
        {
            var vehicles = await _context.ParkedVehicles.ToListAsync();
            return vehicles;
        }

        public async Task<bool> Any(Func<ParkedVehicle, bool> predicate)
        {
            bool any = await Task.Run(() => { return _context.ParkedVehicles.Any(predicate); });
            return any;
        }

        public async Task<IEnumerable<ParkedVehicle>> Find(Func<ParkedVehicle, bool> predicate)
        {
            var vehicles = await Task.Run(() =>
            {
                return _context.ParkedVehicles.Where(predicate).ToList();
            });
            return vehicles;
        }

        public async Task<ParkedVehicle> Create(ParkedVehicle entity)
        {
            var createdVehicle = await _context.ParkedVehicles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return createdVehicle.Entity;
        }

        public async Task<ParkedVehicle?> Delete(int id)
        {
            var vehicleToDelete = await _context.ParkedVehicles.FirstOrDefaultAsync(v => v.Id == id);
            if (vehicleToDelete != null)
            {
                var deletedVehicle = _context.ParkedVehicles.Remove(vehicleToDelete).Entity;
                await _context.SaveChangesAsync();
                return deletedVehicle;
            }
            return null;
        }

        public async Task<ParkedVehicle> Update(ParkedVehicle entity)
        {
            var updatedVehicle = _context.ParkedVehicles.Update(entity).Entity;
            await _context.SaveChangesAsync();
            return updatedVehicle;
        }

      
    }
}
