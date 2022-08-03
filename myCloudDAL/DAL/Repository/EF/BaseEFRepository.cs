using Microsoft.EntityFrameworkCore;
using myCloudDAL.DAL.Interface.Repo;
using System.Linq.Expressions;

namespace myCloudDAL.DAL.Repository.EF
{
    internal abstract class BaseEFRepository<T, Y> : IDisposable, IRepository<T, Y> where T : class where Y : struct
    {
        protected readonly AppDBContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseEFRepository(AppDBContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async virtual Task<bool> CreateAsync(T item)
        {
            await _dbSet.AddAsync(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async virtual Task<bool> DeleteAsync(Y id)
        {
            var item = await GetAsync(id);
            _dbSet.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async virtual Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async virtual Task<IEnumerable<T>> GetAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public abstract Task<T> GetAsync(Y id);

        public async virtual Task<bool> UpdateAsync(T item)
        {
            _dbSet.Update(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
