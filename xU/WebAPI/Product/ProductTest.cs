using Database.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xU.WebAPITest.ProductTests
{
    public class ProductTestsControllerTests : IClassFixture<WebApplicationFactory<WebAPI.Program>>, IDisposable
    {
        private readonly string _controlerName = "Products";
        readonly HttpClient _client;

        public ProductTestsControllerTests(WebApplicationFactory<WebAPI.Program> application)
        {
            _client = application.CreateClient();
        }

        [Fact]
        public async Task GetPetrievesProductsList()
        {
            var response = await _client.GetAsync($"/api/{_controlerName}");
            response.EnsureSuccessStatusCode();
            var expected = "application/json; charset=utf-8";
            var actual = response.Content.Headers.ContentType.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddProduct()
        {
            var expected = new Product()
            {
                Name = "Test name",
                Barcode = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.FFFZ"),
                ParameterOption = new ProductParamtr()
            };
            var content = new StringContent(JsonConvert.SerializeObject(expected), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/{_controlerName}", content);
            response.EnsureSuccessStatusCode();
            var actual = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expected.Name, actual?.Name);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
