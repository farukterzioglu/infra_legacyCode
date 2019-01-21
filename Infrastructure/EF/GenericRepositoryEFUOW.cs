using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.Common;
using Infrastructure.Common.DAL;

namespace Infrastructure.DAL.EF
{
    //TODO : Implement ISearch
    public abstract class GenericRepositoryBaseWithTriggers<T> : IRepositoryWithExpressions<T> where T : class, new()
    {
        private readonly List<T> _identityMappingList;
        private readonly UnitOfWorkContext _unitOfWorkContext;

        protected GenericRepositoryBaseWithTriggers(UnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext ?? new UnitOfWorkContext();

            if (_unitOfWorkContext.IdentityMappingActive)
                _identityMappingList = new List<T>();
        }

        List<IDbTrigger> triggers = null;
        List<IDbTrigger> triggersForThis = null;
        private void GetTriggers()
        {
            //TODO : Implement trigger manager 
            //Get all db triggers
            triggers = null; //TriggerManager.Instance.GetTriggers<IDbTrigger>();

            //Get triggers for this type 
            if (triggers != null && triggers.Any())
                triggersForThis = triggers.Where(x => x.ApplyOn == typeof(T)).ToList();
        }

        public List<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return OnFindById(predicate);
        }

        protected abstract List<T> OnFindById(Expression<Func<T, bool>> predicate);

        public T Add(T obj)
        {
            GetTriggers();

            //Run 'before insert triggers'
            if (triggersForThis != null && triggersForThis.Any())
                foreach (IDbTrigger trigger in triggersForThis)
                {
                    try
                    {
                        trigger.BeforeInsert();
                    }
                    catch (Exception)
                    {
                        //TODO : Log exception
                    }
                }

            //Call to actual insert
            var newObj = OnInsert(obj);

            //Trigger after insert
            if (triggersForThis != null && triggersForThis.Any())
                foreach (IDbTrigger trigger in triggersForThis)
                    try
                    {
                        trigger.AfterInsert();
                    }
                    catch (Exception)
                    {
                        //TODO : Log exception
                    }

            return newObj;
        }

        public void InsertRange(List<T> entities)
        {
            throw new NotImplementedException();
        }

        protected abstract T OnInsert(T obj);

        public void Edit(T obj)
        {
            //@DBTrigger usage
            #region DBTrigger sample
            GetTriggers();
            
            if (triggersForThis != null && triggersForThis.Any())
                foreach (IDbTrigger trigger in triggersForThis)
                trigger.BeforeUpdate();
            #endregion

            OnUpdate(obj);
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        protected abstract void OnUpdate(T obj);

        public void Delete(T obj)
        {
            OnDelete(obj);
        }

        public void DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        protected abstract void OnDelete(T obj);

        public T GetById(object key)
        {
            string idPropName = "Id";
            var idProp = this.GetType().GetProperties().FirstOrDefault( x=> x.GetCustomAttributes(typeof(KeyAttribute),false).Any());
            if (idProp != null) idPropName = idProp.Name;

            //Query '_identityMappingList' if 'IdentityMappingActive'
            if (_unitOfWorkContext.IdentityMappingActive)
            {
                try
                {
                    var param = Expression.Parameter(typeof(T));
                    var lambda1 = Expression.Lambda<Func<T, bool>>(
                        Expression.Equal(
                            Expression.Property(param, "Id"), //That's the harcoded parameter!

                            Expression.Constant(key)),
                        param);

                    var res1 = _identityMappingList.SingleOrDefault<T>(lambda1.Compile());

                    if (res1 != null)
                        return res1;
                    //BinaryExpression equalExpr = Expression.Equal(
                    //    Expression.Property(Expression.Parameter(typeof(T)), idPropName),
                    //    Expression.Constant(id));

                    //var lambda = Expression.Lambda<Func<T, bool>>(equalExpr);

                    //var res = _identityMappingList.SingleOrDefault<T>(lambda.Compile());

                    //if (res != null)
                    //    return res;
                }
                catch (Exception ex)
                {
                    //TODO : remove exception throwing
                    throw ex;
                    // ignored
                }
            }

            T recordFromDB = OnGetById(key);
            if (recordFromDB == null) return null;

            if (!_unitOfWorkContext.IdentityMappingActive) return recordFromDB;

            //Add to identity mapping list if it is active and didn't reached to limit
            if (_unitOfWorkContext.IdentityMappingRecordLimit != null &&_identityMappingList.Count() < _unitOfWorkContext.IdentityMappingRecordLimit)
                _identityMappingList.Add(recordFromDB);

            return recordFromDB;
        }
        protected abstract T OnGetById(object key);

        public List<T> GetAll()
        {
            return OnGetAll();
        }
        protected abstract List<T> OnGetAll();

        public List<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return OnGetAll(includeProperties);
        }
        protected abstract List<T> OnGetAll(params Expression<Func<T, object>>[] includeProperties);

        public List<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return OnGetAll(predicate, includeProperties);
        }
        protected abstract List<T> OnGetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        public T GetOne(object id)
        {
            throw new NotImplementedException();
        }
        
        public List<T> FindByRemoveThis(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<T> obj)
        {
            throw new NotImplementedException();
        }
    }
}
