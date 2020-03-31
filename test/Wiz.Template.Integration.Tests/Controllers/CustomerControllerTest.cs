using AutoBogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wiz.Template.API;
using Wiz.Template.API.Models;
using Xunit;

namespace Wiz.Template.Integration.Tests.Controllers
{
    public class CustomerControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly IAutoFaker _autoFaker;


        public CustomerControllerTest(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
            _autoFaker = AutoFaker.Create();
        }

        [Fact]
        public async Task GetAll_HttpStatusCodeUnauthorizedTestAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/customers");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetById_HttpStatusCodeUnauthorizedTestAsync()
        {
            var id = 1;

            var response = await _httpClient.GetAsync($"/api/v1/customers/{id}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Post_HttpStatusCodeUnauthorizedTestAsync()
        {
            var customer = _autoFaker.Generate<Customer>();

            var response = await _httpClient.PostAsync("/api/v1/customers", new StringContent(JsonConvert.SerializeObject(customer)));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Put_HttpStatusCodeUnauthorizedTestAsync()
        {
            var id = 1;
            var customer = _autoFaker.Generate<Customer>();

            var response = await _httpClient.PutAsync($"/api/v1/customers/{id}", new StringContent(JsonConvert.SerializeObject(customer)));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Delete_HttpStatusCodeUnauthorizedTestAsync()
        {
            var id = 1;
            var response = await _httpClient.DeleteAsync($"/api/v1/customers/{id}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
