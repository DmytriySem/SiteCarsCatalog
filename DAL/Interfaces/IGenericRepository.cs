using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity Get(int id);
        IQueryable<TEntity> Get();
        void Update(TEntity item);
        void Delete(TEntity item);
        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions);
    }
}
