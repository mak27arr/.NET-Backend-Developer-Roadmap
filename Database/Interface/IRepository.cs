namespace Database.Interface
{
    public interface IRepository<T,Y> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(Y id);
        Task<Y> AddAsync(T entity);
        Task<Y> UpdateAsync(T entity);
        Task DeleteAsync(Y id);
        Task SaveAsync();
    }
}
