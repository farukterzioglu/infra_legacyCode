using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Infrastructure.Common.DAL
{
    public interface IRepository<T> where T : class, new()
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        T Add(T entity);
        void AddRange(IEnumerable<T> obj);
        void Delete(T entity);
        void DeleteById(object id);
        void Edit(T entity);
        int Save();
    }
}
