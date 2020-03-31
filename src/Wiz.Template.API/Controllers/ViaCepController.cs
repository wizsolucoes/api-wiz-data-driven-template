using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wiz.Template.API.Models.Services;
using Wiz.Template.API.Services.ViaCEP;

namespace Wiz.Template.API.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/v1/viacep")]
    public class ViaCepController : ControllerBase
    {
        /// <summary>
        /// Busca endereço ou cep.
        /// </summary>
        /// <param name="value">Parâmetro cep do endereço.</param>
        /// <returns>ViaCep.</returns>
        [HttpGet("{value:int}")]
        public async Task<ActionResult<ViaCep>> GetByCep(int value, [FromServices]IViaCepService _viaCepService)
        {
            var viaCep = await _viaCepService.GetByCEPAsync(value);

            if (viaCep is null)
                return NotFound();

            return Ok(viaCep);
        }
    }
}
