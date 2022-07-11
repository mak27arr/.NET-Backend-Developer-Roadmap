using Database.Entities;
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
            var product = new Product()
            {
                Name = "Test name",
                Barcode = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.FFFZ")                   
            };
            var response = await _client.PostAsync($"/api/{_controlerName}", new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
