using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Infrastructure.Domain.CrossCutting.Log;
using Infrastructure.Domain.CrossCutting.Security;
using Infrastructure.AspectOriented.Aspects.Handlers;

namespace Infrastructure.Aspects.HandlerAttributes
{
    public class LogActionSampleAttribute : HandlerAttribute
    {
        private readonly IActionLoger _actionLoger;
        private readonly IUserManager _userManager;

        public LogActionSampleAttribute(IActionLoger actionLoger, IUserManager userManager)
        {
            _actionLoger = actionLoger;
            _userManager = userManager;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new AuditHandler(_actionLoger, _userManager);
        }
    }
}
