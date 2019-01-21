using Infrastructure.Common;
using Infrastructure.Common.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.DAL
{
    public abstract class GenericRepositoryBase<T> : IRepository<T> where T : class, new()
    {
        protected abstract T OnAdd(T entity);
        T IRepository<T>.Add(T entity)
        {
            return this.OnAdd(entity);
        }

        protected abstract void OnDelete(T entity);
        void IRepository<T>.Delete(T entity)
        {
            OnDelete(entity);
        }

        protected abstract void OnEdit(T entity);
        void IRepository<T>.Edit(T entity)
        {
            OnEdit(entity);
        }

        protected abstract IEnumerable<T> OnGetAll();
        IEnumerable<T> IRepository<T>.GetAll()
        {
            return OnGetAll();
        }

        protected abstract int OnSave();
        int IRepository<T>.Save()
        {
            return OnSave();
        }

        protected abstract void OnDeleteById(object id);
        void IRepository<T>.DeleteById(object id)
        {
            OnDeleteById(id);
        }
        
        protected abstract void OnAddRange(IEnumerable<T> obj);
        void IRepository<T>.AddRange(IEnumerable<T> obj)
        {
            OnAddRange(obj);
        }

        protected abstract T OnGetById(object id);
        T IRepository<T>.GetById(object id)
        {
            return OnGetById(id);
        }
    }
}
