using Infrastructure.Common;
using Infrastructure.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Infrastructure.WebApps.Common
{
    public class GenericCRUDApiController<T> : ApiController, ICRUDApiController<T> where T : class, new()
    {
        public readonly IRepository<T> Repository;

        public GenericCRUDApiController(IRepository<T> repository)
        {
            this.Repository = repository;
        }

        [HttpPut]
        [Route("Add")]
        public T Add(T entity)
        {
            return Repository.Add(entity);
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(Repository.GetAll());
        }

        [HttpPost]
        [Route("Update")]
        public void Update(T entity)
        {
            Repository.Edit(entity);
        }

        [HttpPost]
        [Route("Delete")]
        public void Delete(object key)
        {
            Repository.DeleteById(key);
        }

        [HttpGet]
        [Route("GetOne/{id:int}")]
        public T GetOne(int id)
        {
            return Repository.GetById(id);
        }
    }
}