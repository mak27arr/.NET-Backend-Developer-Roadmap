using Database.Entities;
using EF.DAL;
using System;
using System.Linq;
using Xunit;

namespace EFUoWTest
{
    public class ProductRepoTest
    {
        [Fact]
        public async void AddProductTest()
        {
            var expected = GetTestEntity();
            using (var uof = new EFUnitOfWork())
            {
                var id = await uof.Products.AddAsync(expected);
                var actual = await uof.Products.GetAsync(id);
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async void EditProductTest()
        {
            var expected = GetTestEntity();
            using (var uof = new EFUnitOfWork())
            {
                var id = await uof.Products.AddAsync(expected);
                expected.Description = "SuperTest";
                await uof.Products.UpdateAsync(expected);
                await uof.Products.SaveAsync();
                var actual = await uof.Products.GetAsync(id);
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async void DeleteProductTest()
        {
            var expected = GetTestEntity();
            using (var uof = new EFUnitOfWork())
            {
                var id = await uof.Products.AddAsync(expected);
                var actual = await uof.Products.GetAsync(id);
                Assert.NotNull(actual);
                await uof.Products.DeleteAsync(id);
                await uof.Products.SaveAsync();
                actual = await uof.Products.GetAsync(id);
                Assert.Null(actual);
            }
        }

        [Fact]
        public async void GetAllProductTest()
        {
            var expected = GetTestEntity();
            using (var uof = new EFUnitOfWork())
            {
                await uof.Products.AddAsync(expected);
                expected = GetTestEntity();
                await uof.Products.AddAsync(expected);
                expected = GetTestEntity();
                await uof.Products.AddAsync(expected);
                expected = GetTestEntity();
                await uof.Products.AddAsync(expected);
                expected = GetTestEntity();
                await uof.Products.AddAsync(expected);
                var actual = await uof.Products.GetAsync();
                Assert.True(actual?.Count() >= 5);
            }
        }

        private Product GetTestEntity()
        {
            return new Product()
            {
                Name = "TestName",
                AddedOn = DateTime.Now,
                Barcode = "000000",
                Description = "Description"
            };
        }
    }
}