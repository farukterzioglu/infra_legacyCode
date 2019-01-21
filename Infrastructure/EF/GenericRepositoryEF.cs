using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.DAL;
using Infrastructure.Common.DAL;

namespace Infrastructure.DAL.EF
{
    //TODO : Move this to unit test
    public class Test1
    {
        class Model { }
        class SampelRepo : GenericRepositoryEFExecuted<Model>
        { }
        class Context : DbContext{ }

        Test1() {
            SampelRepo.Execute( x=> {
                x.Add(new Model());
                x.Edit(null);
                x.DeleteById(1);
            }, new Context());
        }
    }

    public class GenericRepositoryEFExecuted<T> where T : class, new()
    {
        public static void Execute(Action<IRepository<T>> execution, DbContext dbContext)
        {
            IRepository<T> repo = new GenericRepositoryEF<T>(dbContext);
            execution(repo);
            repo.Save();
        }
    }

    public class GenericRepositoryEF<T> : GenericRepositoryBase<T>, IRepository<T> where T : class, new()
    {
        private readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepositoryEF(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        protected override T OnAdd(T obj)
        {
            return _dbSet.Add(obj);
        }
        
        protected override void OnEdit(T obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        protected override int OnSave()
        {
            return _dbContext.SaveChanges();
        }

        protected override void OnDelete(T obj)
        {
            _dbSet.Remove(obj);
        }

        protected override void OnDeleteById(object id)
        {
            var temp = _dbSet.Find(id);
            _dbSet.Remove(temp);
        }

        protected override IEnumerable<T> OnGetAll()
        {
            IEnumerable<T> items = _dbSet.ToList();

            return items;
        }
        
        protected override void OnAddRange(IEnumerable<T> obj)
        {
            _dbSet.AddRange(obj);
        }

        protected override T OnGetById(object id)
        {
            var tmp = _dbSet.Find(id);
            return tmp;
        }
    }
}
