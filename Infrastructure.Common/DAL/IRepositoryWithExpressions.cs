using Infrastructure.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.DAL
{
    public interface IRepositoryWithExpressions<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
