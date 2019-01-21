namespace Infrastructure.Domain.CrossCutting.Security
{
    public abstract class User : IEntity
    {
        public int Id { get; set; }
        public string UserName;
    }

    public interface IUserManager
    {
        User GetUser();
        bool IsInRole(int roleId);
    }
}
