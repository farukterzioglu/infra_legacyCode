using Microsoft.Practices.Unity.InterceptionExtension;
using Infrastructure.AspectOriented.Aspects.Handlers;
using Infrastructure.Domain.CrossCutting.Log;
using Microsoft.Practices.Unity;

namespace Infrastructure.Aspects.HandlerAttributes
{
    public class LogExceptionAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            ILogger loger = null;

            if (container.IsRegistered<ILogger>())
                loger = container.Resolve<ILogger>();
                
            return new LogExceptionHandler(loger);
        }
    }
}
