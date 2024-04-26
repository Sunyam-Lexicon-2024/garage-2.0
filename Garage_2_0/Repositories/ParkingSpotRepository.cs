using System.Linq.Expressions;
using Garage_2_0.Models;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Repositories
{
    public class ParkingSpotRepository(GarageDbContext context) : IRepository<ParkingSpot>
    {
        private readonly GarageDbContext _context = context;

        public async Task<IEnumerable<ParkingSpot>> All()
        {
            var vehicles = await _context.ParkingSpots.ToListAsync();
            return vehicles;
        }

        public async Task<bool> Any(Expression<Func<ParkingSpot, bool>> predicate)
        {
            bool any = await Task.Run(() => { return _context.ParkingSpots.Any(predicate); });
            return any;
        }

        public async Task<IEnumerable<ParkingSpot>> Find(Expression<Func<ParkingSpot, bool>> predicate)
        {
            var vehicles = await Task.Run(() =>
            {
                return _context.ParkingSpots.Where(predicate).ToList();
            });
            return vehicles;
        }

        public async Task<ParkingSpot> Create(ParkingSpot entity)
        {
            var createdParkingSpot = (await _context.ParkingSpots.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();
            return createdParkingSpot;
        }

        public async Task<ParkingSpot?> Delete(int id)
        {
            var parkingSpotToDelete = await _context.ParkingSpots.FirstOrDefaultAsync(v => v.Id == id);
            if (parkingSpotToDelete != null)
            {
                var deletedParkingSpot = _context.ParkingSpots.Remove(parkingSpotToDelete).Entity;
                await _context.SaveChangesAsync();
                return deletedParkingSpot;
            }
            return null;
        }

        public async Task<ParkingSpot> Update(ParkingSpot entity)
        {
            var updatedParkingSpot = _context.ParkingSpots.Update(entity).Entity;
            await _context.SaveChangesAsync();
            return updatedParkingSpot;
        }

        public IQueryable<ParkingSpot> GetManyToManyRelation(int id)
        {
            return from vehicle in _context.Vehicles
                   where vehicle.Id == id
                   from parkingSpot in vehicle.ParkingSpots
                   select parkingSpot;
        }
    }
}
