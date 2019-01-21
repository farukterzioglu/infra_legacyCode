using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Domain.CrossCutting.ExceptionHandling
{
    public interface IHandler
    {
        void HandleException(Action action);
        void LogException(Action action);

        void HandleExceptionAsync(Func<Task> action);
        void HandleExceptionAsync(Func<Task> action, CancellationToken cancellationToken);
        
        void LogExceptionAsync(Func<Task> action);
        void LogExceptionAsync(Func<Task> action, CancellationToken cancellationToken);
    }
}
