namespace Garage_2_0.Repositories;

public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> All();
    public Task<IEnumerable<T>> Find(Func<T, bool> predicate);
    public Task<T> Create(T entity);
    public Task<T> Update(T entity);
    public Task<T?> Delete(int id);
    public Task<bool> Any(Func<T, bool> predicate);
    public IQueryable<T> GetManyToManyRelation(int id);

}
