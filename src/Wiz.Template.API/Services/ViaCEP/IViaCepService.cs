using System.Threading.Tasks;
using Wiz.Template.API.Models.Services;

namespace Wiz.Template.API.Services.ViaCEP
{
    public interface IViaCepService
    {
        Task<ViaCep> GetByCEPAsync(int cep);
    }
}
