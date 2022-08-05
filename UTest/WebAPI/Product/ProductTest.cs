using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace UTest.WebAPITest.Product
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
            var response = await _client.GetAsync($"/{_controlerName}");
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public void GetPetrievesProductsList1()
        {
            Assert.Equal(true, false);
        }
    }
}
