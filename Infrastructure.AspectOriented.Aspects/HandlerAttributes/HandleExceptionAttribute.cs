using Infrastructure.AspectOriented.Aspects.Handlers;
using Infrastructure.Domain.CrossCutting.Log;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Infrastructure.AspectOriented.Aspects.HandlerAttributes
{
    public class HandleExceptionAttribute : HandlerAttribute
    {
        private bool _handleException;
        public bool HandleException { get { return _handleException; } set { _handleException = value; } }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            ILogger loger = null;

            if (container.IsRegistered<ILogger>())
                loger = container.Resolve<ILogger>();

            return new HandleExceptionHandler(loger, _handleException);
        }
    }
}
