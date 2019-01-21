using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Security
{
    public class NotAuthorizedException : Exception
    {
        public string ExceptionMessage;
        public NotAuthorizedException(string exceptionMessage)
        {
            ExceptionMessage = exceptionMessage;
        }
    }
}
