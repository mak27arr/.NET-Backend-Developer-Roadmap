using System.Linq.Expressions;

namespace myCloudDAL.DAL.Interface.Repo
{
    public interface IRepository<T, ID> where T : class where ID : struct
    {
        Task<IQueryable<T>> GetAsync();
        Task<T> GetAsync(ID id);
        Task<IQueryable<T>> FindAsync(Expression<Func<T, Boolean>> predicate);
        Task<bool> CreateAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(ID id);
    }
}
