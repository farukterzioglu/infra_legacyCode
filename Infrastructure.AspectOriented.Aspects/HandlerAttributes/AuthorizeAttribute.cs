using Infrastructure.AspectOriented.Aspects.Handlers;
using Infrastructure.Domain.CrossCutting;
using Infrastructure.Domain.CrossCutting.Log;
using Infrastructure.Domain.CrossCutting.Security;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Infrastructure.AspectOriented.Aspects.HandlerAttributes
{
    public class AuthorizeAttribute : HandlerAttribute
    {
        private readonly IUserManager _userManager;
        private readonly int _roleIdToBeChecked;

        public AuthorizeAttribute(int roleIdToBeChecked)
        {
            //TODO : Inject IUserManager 
            //_userManager = UnityConfig.GetConfiguredContainer().Resolve<IUserManager>();
            _roleIdToBeChecked = roleIdToBeChecked;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new AuthorizeHandler(_userManager, _roleIdToBeChecked);
        }
    }   
}
