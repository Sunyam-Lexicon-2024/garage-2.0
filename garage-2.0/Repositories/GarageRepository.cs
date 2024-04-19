using garage_2._0.Models;
using Garage_2._0.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Repositories;

public class GarageRepository(GarageDbContext context) : IRepository<Garage>
{

    private GarageDbContext _context = context;

    public async Task<IEnumerable<Garage>> All()
    {
        var garages = await _context.Garages.ToListAsync();
        return garages;
    }
    
    public async Task<bool> Any(Func<Garage, bool> predicate)
    {
        bool any = await Task.Run(() => { return _context.Garages.Any(predicate); });
        return any;
    }

    public async Task<IEnumerable<Garage>> Find(Func<Garage, bool> predicate)
    {
        var garages = await Task.Run(() =>
        {
            return _context.Garages.Where(predicate).ToList();
        });
        return garages;
    }

    public async Task<Garage> Create(Garage entity)
    {
        var createdGarage = await _context.Garages.AddAsync(entity);
        await _context.SaveChangesAsync();
        return createdGarage.Entity;
    }

    public async Task<Garage?> Delete(int id)
    {
        var garageToDelete = await _context.Garages.FirstOrDefaultAsync(g => g.ID == id);
        if (garageToDelete != null)
        {
            var deletedGarage = _context.Garages.Remove(garageToDelete).Entity;
            await _context.SaveChangesAsync();
            return deletedGarage;
        }
        return null;
    }

    public async Task<Garage> Update(Garage entity)
    {
        var updatedGarage = _context.Garages.Update(entity).Entity;
        await _context.SaveChangesAsync();
        return updatedGarage;
    }
}