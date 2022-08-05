using Dapper;
using Database.Entities;
using Database.Interface;
using System.Data.SqlClient;

namespace DappeRTest.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public ProductRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Product> GetAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            try
            {
                _connection.Open();
                return await _connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            var sql = "SELECT * FROM Products";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                return await connection.QueryAsync<Product>(sql);
            }
        }

        public async Task<int> AddAsync(Product entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Products (Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }
    }
}
