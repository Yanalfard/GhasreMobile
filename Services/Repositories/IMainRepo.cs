using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Services.Repositories
{
    public interface IMainRepo<TEntity> where TEntity : class
    {
        EntityEntry Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool DeleteById(object id);
        TEntity GetById(object id);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includes = null);
    }
}
