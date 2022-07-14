using Database.Interface;
using EF.DAL.Context;
using EF.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF.DAL
{
    public class EFUnitOfWork : IUnitOfWork //internal
    {
        private const string _settingFileName = "appsettings.json";
        private string _connectionString;
        private ProductContext _productContext;
        private IProductRepository _productRepository;

        private ProductContext ProductContext
        {
            get
            {
                if (_productContext == null)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
                    var options = optionsBuilder
                        .UseLazyLoadingProxies()
                        .UseSqlServer(_connectionString)
                        .UseLoggerFactory(new EFLoggerFactory())
                        .Options;
                    _productContext = new ProductContext(options);
                }

                return _productContext;
            }
        }

        public IProductRepository Products 
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(ProductContext);

                return _productRepository;
            }
        }

        public EFUnitOfWork()
        {
            GetSettings();
        }

        private void GetSettings()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(_settingFileName);
            var config = builder.Build();
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public void Dispose()
        {
            _productContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _productRepository.SaveAsync();
        }
    }
}
