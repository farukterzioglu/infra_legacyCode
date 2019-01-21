using Infrastructure.AspectOriented.Aspects.HandlerAttributes;
using Infrastructure.Domain.CrossCutting.ExceptionHandling;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.ExceptionHandling
{
    public class Handler : IHandler
    {
        [HandleException(HandleException = true)]
        void IHandler.HandleException(Action action)
        {
            action();
        }

        [HandleException(HandleException = true)]
        void IHandler.HandleExceptionAsync(Func<Task> action)
        {
            action().Wait();
        }

        [HandleException(HandleException = true)]
        void IHandler.HandleExceptionAsync(Func<Task> action, CancellationToken cancellationToken)
        {
            action().Wait(cancellationToken: cancellationToken);
        }

        [HandleException(HandleException = false)]
        void IHandler.LogException(Action action)
        {
            action();
        }

        [HandleException(HandleException = false)]
        void IHandler.LogExceptionAsync(Func<Task> action)
        {
            action().Wait();
        }

        [HandleException(HandleException = false)]
        void IHandler.LogExceptionAsync(Func<Task> action, CancellationToken cancellationToken)
        {
            action().Wait(cancellationToken);
        }
    }
}
