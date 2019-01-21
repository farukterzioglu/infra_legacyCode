using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Infrastructure.AspectOriented.Aspects.Handlers;

namespace Infrastructure.Aspects.HandlerAttributes
{
    public abstract class TraceSampleAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new TraceCallHandler();
        }
    }
}
