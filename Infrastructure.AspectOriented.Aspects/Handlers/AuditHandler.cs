using System;
using Microsoft.Practices.Unity.InterceptionExtension;
using Infrastructure.Domain.CrossCutting.Security;
using Infrastructure.Domain.CrossCutting.Log;

namespace Infrastructure.AspectOriented.Aspects.Handlers
{
    public class AuditHandler : ICallHandler
    {
        private readonly IActionLoger _actionLoger;
        private readonly IUserManager _userManager;

        public AuditHandler(IActionLoger actionLoger, IUserManager userManager)
        {
            _actionLoger = actionLoger;
            _userManager = userManager;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            // Before invoking the method on the original target.
            var sampleLog = (String.Format(
                "Invoking method {0} at {1}",
                input.MethodBase, DateTime.Now.ToLongTimeString()));

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            if (result.Exception != null)
            {
                sampleLog = (String.Format(
                    "Method {0} threw exception {1} at {2}",
                    input.MethodBase, result.Exception.Message,
                    DateTime.Now.ToLongTimeString()));
            }
            else
            {
                sampleLog = (String.Format(
                    "Method {0} returned {1} at {2}",
                    input.MethodBase, result.ReturnValue,
                    DateTime.Now.ToLongTimeString()));
            }

            _actionLoger.LogAction(
                _userManager.GetUser().UserName,
                input.MethodBase.Name, null, null, result.ReturnValue);

            return result;
        }

        public int Order { get; set; }
        
    }
}
