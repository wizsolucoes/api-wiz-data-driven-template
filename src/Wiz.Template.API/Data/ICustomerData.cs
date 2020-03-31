using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Data.Base;
using Wiz.Template.API.Models;
using Wiz.Template.API.Models.Dapper;

namespace Wiz.Template.API.Data
{
    public interface ICustomerData : IEntityBaseData<Customer>, IDapperReadData<CustomerDapper>
    {
        Task<IEnumerable<CustomerDapper>> GetAllAsync();
    }
}
