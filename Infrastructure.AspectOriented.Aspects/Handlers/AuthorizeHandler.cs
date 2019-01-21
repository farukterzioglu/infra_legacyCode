using Infrastructure.Domain.CrossCutting.Security;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Infrastructure.AspectOriented.Aspects.Handlers
{
    public class AuthorizeHandler : ICallHandler
    {
        private readonly IUserManager _userManager;
        private readonly int _roleIdToBeChecked ;

        public AuthorizeHandler(IUserManager userManager, int roleIdToBeChecked)
        {
            _userManager = userManager;
            _roleIdToBeChecked = roleIdToBeChecked;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //Check role, throw error if doesn't have
            var user = _userManager.GetUser();

            if (!_userManager.IsInRole(_roleIdToBeChecked))
                throw new NotAuthorizedException(user.UserName + " unauthorized  to invoke " + input.MethodBase.Name);

            //Invoke method
            var result = getNext()(input, getNext);
            
            return result;
        }

        public int Order { get; set; }
    }
}
