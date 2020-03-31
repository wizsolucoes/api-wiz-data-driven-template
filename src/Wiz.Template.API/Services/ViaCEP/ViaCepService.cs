using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wiz.Template.API.Models.Services;
using Wiz.Template.API.Services.Notifications;

namespace Wiz.Template.API.Services.ViaCEP
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;
        private readonly IDomainNotification _domainNotification;

        public ViaCepService(HttpClient httpClient, IDomainNotification domainNotification)
        {
            _httpClient = httpClient;
            _domainNotification = domainNotification;
        }

        public async Task<ViaCep> GetByCEPAsync(int cep)
        {
            var response = await _httpClient.GetAsync($"{cep}/json");
            var stringResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ViaCep>(stringResponse);
            }

            _domainNotification.AddNotification(HttpStatusCode.BadRequest.ToString(), "Cep não encontrado!", true);
            return null;
        }
    }
}
