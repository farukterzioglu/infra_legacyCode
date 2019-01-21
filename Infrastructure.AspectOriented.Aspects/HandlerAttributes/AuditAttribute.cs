using Infrastructure.AspectOriented.Aspects.Handlers;
using Infrastructure.Domain.CrossCutting.Log;
using Infrastructure.Domain.CrossCutting.Security;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Infrastructure.AspectOriented.Aspects.HandlerAttributes
{
    public class AuditAttribute : HandlerAttribute
    {
        public AuditAttribute(int order)
        {
            base.Order = order;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            var actionLoger = container.Resolve<IActionLoger>();
            var userManager = container.Resolve<IUserManager>();

            return new AuditHandler(actionLoger, userManager);
        }
    }
}
