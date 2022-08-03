using System.Linq.Expressions;

namespace myCloudDAL.DAL.Interface.Repo
{
    internal interface IRepository<T, ID> where T : class where ID : struct
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(ID id);
        Task<IList<T>> FindAsync(Expression<Func<T, Boolean>> predicate);
        Task<bool> CreateAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(ID id);
    }
}
