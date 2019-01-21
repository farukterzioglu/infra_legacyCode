using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Infrastructure.WebApps.Common
{
    public interface ICRUDApiController<T>
    {
        T Add(T entity);
        IHttpActionResult GetAll();
        void Update(T entity);
        void Delete(object key);
        T GetOne(int id);
    }
}
