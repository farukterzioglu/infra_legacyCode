using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;
using Infrastructure.Common.DAL;

namespace Infrastructure.DAL.Fake
{
    public abstract class AbstractFakeAsyncRepo<T> : IAsyncRepository<T> where T : class
    {
        protected readonly List<T> DataSource = new List<T>();

        protected AbstractFakeAsyncRepo(List<T> initData)
        {
            DataSource.AddRange(initData);
        }

        public Task<List<T>> GetAllAsync()
        {
            return Task.Run(() => DataSource);
        }

        public abstract Task<T> AddAsync(T entity);
        public abstract Task DeleteAsync(object key);
        public abstract Task UpdateAsync(T entity);
    }
}
