using garage_2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace garage_2._0.Repositories
{
    public class ParkedVehicleRepository(GarageDbContext context) : IRepository<ParkedVehicle>
    {
        private readonly GarageDbContext _context = context;

        public async Task<IEnumerable<ParkedVehicle>> All()
        {
            var vehicles = await _context.ParkedVehicles.ToListAsync();
            return vehicles;
        }

        public bool Any(int id) => _context.ParkedVehicles.Any(x => x.Id == id);

        public async Task Create(ParkedVehicle entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var vehicle = await _context.ParkedVehicles.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicle is null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            _context.Remove(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<ParkedVehicle> GetById(int id)
        {
            var vehicle = await _context.ParkedVehicles.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicle is null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            return vehicle;
        }

        public Task Update(ParkedVehicle entity)
        {
            throw new NotImplementedException();
        }
    }
}
