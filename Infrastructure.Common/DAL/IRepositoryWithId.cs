using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.DAL
{
    public interface IRepositoryWithId<T> : IRepository<T> where T : class, IEntity, new()
    {
        T GetById(int id);
    }

    public interface IRepositoryWithLongId<T> : IRepository<T> where T : class, IEntityLong, new()
    {
        T GetById(long id);
    }
}
