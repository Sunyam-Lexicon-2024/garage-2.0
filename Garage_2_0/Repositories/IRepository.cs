using System.Linq.Expressions;

namespace Garage_2_0.Repositories;

public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> All();
    public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    public Task<T> Create(T entity);
    public Task<T> Update(T entity);
    public Task<T?> Delete(int id);
    public Task<bool> Any(Expression<Func<T, bool>> predicate);
    
}
