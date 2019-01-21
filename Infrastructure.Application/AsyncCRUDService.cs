using Infrastructure.Common;
using Infrastructure.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Application
{
    public class AsyncService<T> : IAsyncService<T> where T : class
    {
        protected readonly IAsyncRepository<T> Repository;

        public AsyncService(IAsyncRepository<T> repository)
        {
            Repository = repository;
        }

        public Task<List<T>> GetAllAsync()
        {
            return Repository.GetAllAsync();
        }

        public Task<T> AddAsync(T entity)
        {
            return Repository.AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            return Repository.UpdateAsync(entity);
        }

        public Task DeleteAsync(int key)
        {
            return Repository.DeleteAsync(key);
        }
    }
}
