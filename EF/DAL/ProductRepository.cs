using Database.Entities;
using Database.Interface;
using EF.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace EF.DAL
{
    internal class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;

            if (!_context.Database.CanConnect())
                Console.WriteLine($"{nameof(ProductContext)} unavailable");
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<int> AddAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await SaveAsync();
            return entity.Id;
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            return await Task.FromResult(_context.Products.Update(entity).Entity.Id);
        }

        public async Task DeleteAsync(int id)
        {
            _context.Products.Remove(await GetAsync(id));
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
