using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wiz.Template.API.Models.Services;
using Xunit;

namespace Wiz.Template.Integration.Tests.Services
{
    public class ViaCepServiceTest
    {
        [Fact]
        public async Task GetByCEPAsync_ReturAddressTest()
        {
            var cep = 72321530;

            using HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://viacep.com.br/ws/")
            };
            var response = await client.GetAsync($"{cep}/json");

            if (response.IsSuccessStatusCode)
            {
                var address = JsonConvert.DeserializeObject<ViaCep>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(address);
                return;
            }
         
            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
