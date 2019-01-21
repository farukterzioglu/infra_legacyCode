using System.Data.Entity;

namespace Infrastructure.DAL.EF
{
    public abstract class DbContextFactory
    {
        public abstract DbContext GetDbContext();
        public static DbContext GetNewDbContext<TDbContext>() where TDbContext : DbContext, new()
        {
            return new TDbContext();
        }
    }
}
