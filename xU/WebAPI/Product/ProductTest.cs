using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace xU.WebAPITest.Product
{
    public class ProductTestsControllerTests : IClassFixture<WebApplicationFactory<WebAPI.Program>>
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
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
