using System;

namespace Wiz.Template.API.Data.Base
{
    public interface IEntityBaseData<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        TEntity AddWithReturn(TEntity obj);
        void Update(TEntity obj);
        void Remove(TEntity obj);
    }
}
