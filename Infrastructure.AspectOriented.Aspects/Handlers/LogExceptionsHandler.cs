using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;
using Infrastructure.Domain.CrossCutting.Log;

namespace Infrastructure.AspectOriented.Aspects.Handlers
{
    public class LogExceptionHandler : ICallHandler
    {
        private readonly ILogger _loger;
        public int Order { get; set; }

        public LogExceptionHandler(ILogger loger)
        {
            _loger = loger;
        }
        
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var methodReturn = getNext().Invoke(input, getNext);
            if (methodReturn.Exception != null)
            {
                if (_loger != null)
                {
                    _loger.Log(new LogMessage() { Message = methodReturn.Exception.Message }, Category.Exception, Priority.None);
                    if (methodReturn.Exception.InnerException != null) _loger.Log(new LogMessage() {  Message = methodReturn.Exception.InnerException.Message }, Category.Exception, Priority.None);
                }
                return methodReturn;
            }
            return methodReturn;
        }
    }
}
