using Infrastructure.Domain.CrossCutting.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Log
{
    public class ConsoleLogger : ILogger
    {
        public void Log(Audit audit)
        {
            throw new NotImplementedException();
        }

        public void Log(LogMessage message, Category category = Category.Exception, Priority priority = Priority.Medium, ExceptionDetails exceptionDetails = null)
        {
            throw new NotImplementedException();
        }
    }
}
