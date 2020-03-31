using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Data.Base;
using Wiz.Template.API.Infra.Context;
using Wiz.Template.API.Models;
using Wiz.Template.API.Models.Dapper;

namespace Wiz.Template.API.Data
{
    public class CustomerData : EntityBaseData<Customer>, ICustomerData
    {
        private readonly DapperContext _dapperContext;

        public CustomerData(EntityContext context, DapperContext dapperContext)
            : base(context)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<CustomerDapper>> GetAllAsync()
        {
            var query = @"SELECT Id, Name, DateCreated
                          FROM dbo.Customer";

            return await _dapperContext.DapperConnection.QueryAsync<CustomerDapper>(query);
        }

        public async Task<CustomerDapper> GetByIdAsync(int id)
        {
            var query = @"SELECT Id, Name, DateCreated
                          FROM dbo.Customer
                          WHERE Id = @Id";

            return await _dapperContext.DapperConnection.QueryFirstOrDefaultAsync<CustomerDapper>(query, new { Id = id });
        }
    }
}
