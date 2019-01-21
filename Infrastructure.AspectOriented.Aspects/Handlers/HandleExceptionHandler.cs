using Infrastructure.Domain.CrossCutting.Log;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AspectOriented.Aspects.Handlers
{
    public class HandleExceptionHandler : ICallHandler
    {
        private readonly bool _handleException = false;
        private readonly ILogger _ILogger;

        public HandleExceptionHandler(ILogger ILogger, bool handleException)
        {
            _ILogger = ILogger;
            _handleException = handleException;
        }

        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var methodReturn = getNext().Invoke(input, getNext);

            #region custom logic 

            // AFTER the target method execution check to see if it excepted 
            if (methodReturn.Exception != null)
            {
                if (_ILogger != null)
                {
                    _ILogger.Log(new LogMessage() { Message = methodReturn.Exception.Message }, Category.Exception, Priority.None);
                    if (methodReturn.Exception.InnerException != null) _ILogger.Log(new LogMessage() { Message = methodReturn.Exception.InnerException.Message }, Category.Exception, Priority.None);
                }

                //Handle or not 
                if (_handleException) methodReturn.Exception = null;
            }
            #endregion
            return methodReturn;
        }
    }
}
