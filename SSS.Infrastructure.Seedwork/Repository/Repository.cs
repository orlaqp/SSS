using Microsoft.EntityFrameworkCore;
using SSS.Domain.Seedwork.Repository;
using SSS.Infrastructure.Seedwork.DbContext;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SSS.Infrastructure.Seedwork.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbcontextBase Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DbcontextBase context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual TEntity Get(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<TEntity> GetPage(int index, int size, ref int count)
        {
            count = DbSet.Count();
            return DbSet.Skip(size * (index > 0 ? index - 1 : 0)).Take(size);
        }

        public IQueryable<TEntity> GetPage(int index, int size, Expression<Func<TEntity, bool>> predicate, ref int count)
        {
            count = DbSet.Where(predicate).Count();
            return DbSet.Where(predicate).Skip(size * (index > 0 ? index - 1 : 0)).Take(size);
        }

        public virtual void Update(TEntity obj)
        {
            var status = DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
