using Database.Entities;

namespace Database.Interface
{
    public interface IProductRepository : IRepository<Product, int>, IDisposable
    {
    }
}
