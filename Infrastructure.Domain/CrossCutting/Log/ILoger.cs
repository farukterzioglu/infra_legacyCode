using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain.CrossCutting.Log
{
    public class LogMessage
    {
        public string Message { get; set; }
    }
    public class Audit : LogMessage
    {
        public int UserId { get; set; }
        public int LogType { get; set; }
    }

    public class ExceptionDetails
    {
        public string NameSpace;
        public string ClassName;
        public string MethodName;
        public Dictionary<string, object> Parameters;
    }

    public interface ILogger
    {
        void Log(
            LogMessage message,
            Category category = Category.Exception,
            Priority priority = Priority.Medium,
            ExceptionDetails exceptionDetails = null);
        void Log(Audit audit);
    }
}
