using garage_2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace garage_2._0.Repositories
{
    public class GarageRepository(GarageDbContext context) : IRepository<Garage>
    {
        private readonly GarageDbContext _context = context;

        public async Task<Garage?> GetById(int? id)
        {
            var garage = await _context.Garages.FirstOrDefaultAsync(g => g.ID == id);

            return garage;
        }

        public async Task Update(Garage garage)
        {
            _context.Update(garage);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var garage = await GetById(id);
            if (garage != null)
            {
                _context.Garages.Remove(garage);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Garage?>> All()
        {
            var garages = await _context.Garages.ToListAsync();

            return garages;
        }

        public bool Any(int? id)
        {
            return _context.Garages.Any(g => g.ID == id);
        }

    }
}
