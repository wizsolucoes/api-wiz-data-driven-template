using System.Threading.Tasks;

namespace Wiz.Template.API.Data.Base
{
    public interface IDapperReadData<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
    }
}
