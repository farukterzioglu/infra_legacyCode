using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.DAL
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task DeleteAsync(object key);
        Task UpdateAsync(T entity);
        Task<T> AddAsync(T entity);
    }
}
