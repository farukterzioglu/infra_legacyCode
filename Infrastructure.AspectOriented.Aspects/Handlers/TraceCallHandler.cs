using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Infrastructure.AspectOriented.Aspects.Handlers
{
    public class TraceCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Console.WriteLine("Called: {0}.{1}", input.Target.GetType(), input.MethodBase.Name);
            if (input.Arguments.Count > 0)
            {
                Console.WriteLine("\tWith Arguments");
                for (int i = 0; i < input.Arguments.Count; i++)
                {
                    Console.WriteLine("\t\tName:" + input.Arguments.ParameterName(i));
                }
            }

            InvokeHandlerDelegate next = getNext();
            Console.WriteLine("Execute...");
            IMethodReturn methodReturn = next(input, getNext);

            string result = methodReturn.ReturnValue == null ? "(void)" : methodReturn.ReturnValue.ToString();

            if (methodReturn.Exception != null)
                Console.WriteLine("Exception: {0}", methodReturn.Exception);

            Console.WriteLine("Result: {0}", result);

            return methodReturn;
        }

        public int Order { get; set; }
    }
}
