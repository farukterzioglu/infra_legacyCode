using Infrastructure.AspectOriented.Aspects.Handlers;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AspectOriented.Aspects.HandlerAttributes
{
    public class TraceAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new TraceCallHandler();
        }
    }
}
