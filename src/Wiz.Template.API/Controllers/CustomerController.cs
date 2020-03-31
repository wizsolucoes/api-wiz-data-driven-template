using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Data;
using Wiz.Template.API.Infra.UoW;
using Wiz.Template.API.Models;
using Wiz.Template.API.Models.Dapper;

namespace Wiz.Template.API.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/v1/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerData _customerData;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(ICustomerData customerData, IUnitOfWork unitOfWork)
        {
            _customerData = customerData;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lista de clientes.
        /// </summary>
        /// <returns>Clientes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDapper>>> GetAll()
        {
            return Ok(await _customerData.GetAllAsync());
        }

        /// <summary>
        /// Cliente por Id.
        /// </summary>
        /// <param name="customer">Parâmetro "id" do cliente.</param>
        /// <returns>Cliente.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDapper>> GetById(int id)
        {
            var customerDapper = await _customerData.GetByIdAsync(id);

            if (customerDapper is null)
                return NotFound();

            return Ok(customerDapper);
        }

        /// <summary>
        /// Criação de cliente.
        /// </summary>
        /// <param name="customer">Parâmetro "cliente".</param>
        /// <returns>Cliente criado.</returns>
        [HttpPost]
        public ActionResult<Customer> Post([FromBody]Customer customer)
        {
            var entity = _customerData.AddWithReturn(customer);
            _unitOfWork.Commit();

            return Created(nameof(GetById), entity);
        }

        /// <summary>
        /// Atualização de cliente.
        /// </summary>
        /// <param name="id">Parâmetro "id" do cliente.</param>
        /// <param name="customer">Parâmetro "cliente".</param>
        /// <returns>Cliente atualizado.</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody]Customer customer)
        {
            if (customer?.Id != id)
                return BadRequest();

            var customerDapper = await _customerData.GetByIdAsync(id);

            if (customerDapper is null)
                return NotFound();

            _customerData.Update(customer);
            _unitOfWork.Commit();

            return NoContent();
        }

        /// <summary>
        /// Exclusão de cliente.
        /// </summary>
        /// <param name="customer">Parâmetro "id" do cliente.</param>
        /// <returns>Cliente excluido.</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var customerDapper = await _customerData.GetByIdAsync(id);

            if (customerDapper is null)
                return NotFound();

            var customer = new Customer(customerDapper);

            _customerData.Remove(customer);
            _unitOfWork.Commit();

            return NoContent();
        }
    }
}
