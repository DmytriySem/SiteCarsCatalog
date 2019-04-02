using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private CatalogContext ctx;
        private DbSet<TEntity> dbSet;

        public EFGenericRepository()
        {
            ctx = new CatalogContext();
            dbSet = ctx.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            dbSet.Add(item);
            ctx.SaveChanges();
        }

        public void Delete(TEntity item)
        {
            ctx.Entry(item).State = EntityState.Deleted;
            ctx.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return dbSet.Find(id);
        }

        public IQueryable<TEntity> Get()
        {
            return dbSet.AsNoTracking();
        }

        public void Update(TEntity item)
        {
            dbSet.Attach(item);
            var entry = ctx.Entry(item);
            entry.State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> query = null;
            foreach (var includeExpression in includeExpressions)
            {
                query = dbSet.Include(includeExpression);
            }

            return query ?? dbSet;
        }
    }
}
