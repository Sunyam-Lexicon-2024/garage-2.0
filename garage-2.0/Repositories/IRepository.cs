namespace garage_2._0.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<T?> GetById(int id);
        public Task Update(T entity);
        public Task Delete(int id);
        public Task<IEnumerable<T?>> All();
        public bool Any(int id);

    }
}
