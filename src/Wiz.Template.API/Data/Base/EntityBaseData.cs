using Microsoft.EntityFrameworkCore;
using System;
using Wiz.Template.API.Infra.Context;

namespace Wiz.Template.API.Data.Base
{
    public class EntityBaseData<TEntity> : IEntityBaseData<TEntity> where TEntity : class
    {
        protected readonly EntityContext Db;
        protected readonly DbSet<TEntity> DbSet;
        private bool disposedValue = false;

        public EntityBaseData(EntityContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }        

        public TEntity AddWithReturn(TEntity obj)
        {
            return DbSet.Add(obj).Entity;
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(TEntity obj)
        {
            DbSet.Remove(obj);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && Db != null)
                    Db.Dispose();

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
