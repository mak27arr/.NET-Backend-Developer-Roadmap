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

        public virtual Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return Task.FromResult(_dbSet.Where(predicate).AsNoTracking());
        }

        public virtual Task<IQueryable<T>> GetAsync()
        {
            return Task.FromResult(_dbSet.AsNoTracking());
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
