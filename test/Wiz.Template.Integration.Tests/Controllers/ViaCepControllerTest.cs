using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wiz.Template.API;
using Xunit;

namespace Wiz.Template.Integration.Tests.Controllers
{
    public class ViaCepControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public ViaCepControllerTest(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_HttpStatusCodeUnauthorizedTestAsync()
        {
            var cep = 72321530;

            var response = await _httpClient.GetAsync($"api/v1/viacep/{cep}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

    }
}
