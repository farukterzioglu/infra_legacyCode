using Infrastructure.Common;
using Infrastructure.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Fake
{
    public class GenericFakeAsyncRepoWithEntity<T> : AbstractFakeAsyncRepo<T> where T : class, IEntity 
    {
        public GenericFakeAsyncRepoWithEntity(List<T> initData) : base(initData)
        {
        }

        public override Task<T> AddAsync(T entity)
        {
            return Task.Run(() =>
            {
                entity.Id= GetAllAsync().Result.Count + 1;
                DataSource.Add(entity);
                return entity;
            });
        }
        public override Task DeleteAsync(object key)
        {
            return Task.Run(() =>
            {
                var data = GetAllAsync().Result.FirstOrDefault(x => x.Id == (int)key);

                if (data != null)
                {
                    DataSource.Remove(data);
                }
            });
        }
        public override Task UpdateAsync(T entity)
        {
            return Task.Run(() =>
            {
                var data = GetAllAsync().Result.FirstOrDefault(x => x.Id == entity.Id);

                if (data != null)
                {
                    DataSource.Remove(data);
                    DataSource.Add(entity);
                }
            });
        }
    }
}
