using Infrastructure.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Infrastructure.DAL.EF
{
    //TODO : Unit test this 
    public class GenericRepositoryEFWithExpression<T> : GenericRepositoryEF<T>, IRepository<T>, IRepositoryWithExpressions<T> where T : class, new()
    {
        public GenericRepositoryEFWithExpression(DbContext dbContext) : base(dbContext)
        {
        }

        public List<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public List<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            foreach (var item in includeProperties)
            {
                query = query.Include(item);
            }
            return query.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsQueryable().Where(predicate);

            foreach (var item in includeProperties)
            {
                query = query.Include(item);
            }
            return query.ToList();
        }
    }
}
