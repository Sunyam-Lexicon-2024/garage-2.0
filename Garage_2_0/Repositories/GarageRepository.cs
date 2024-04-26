using Garage_2_0.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Repositories;

public class GarageRepository(GarageDbContext context) : IRepository<Garage>
{

    private readonly GarageDbContext _context = context;

    public async Task<IEnumerable<Garage>> All()
    {
        var garages = await _context.Garages.ToListAsync();
        return garages;
    }

    public async Task<bool> Any(Expression<Func<Garage, bool>> predicate)
    {
        bool any = await Task.Run(() => { return _context.Garages.Any(predicate); });
        return any;
    }

    public async Task<IEnumerable<Garage>> Find(Expression<Func<Garage, bool>> predicate)
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
        var garageToDelete = await _context.Garages.FirstOrDefaultAsync(g => g.Id == id);
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

    public IQueryable<Garage> GetManyToManyRelation(int id)
    {
        return from garage in _context.Garages
               where garage.Id == id
               select garage;
    }
}
