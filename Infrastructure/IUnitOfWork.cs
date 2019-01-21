using Infrastructure.Common;
using Infrastructure.Common.DAL;
using System;

namespace Infrastructure.DAL
{
    public class UnitOfWorkContext
    {
        public bool IdentityMappingActive = false;
        public int? IdentityMappingRecordLimit = null;
    }

    public interface IUnitOfWork : IDisposable
    {
        UnitOfWorkContext UnitOfWorkContext { get; }

        void Commit();
        void Roolback();
        int SaveChanges();
        void Dispose(bool disposing);
    }

    public interface IUnitOfWorkGeneric : IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class , new();    
    }
}
