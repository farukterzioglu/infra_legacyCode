using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.PluginFramework.Core
{
    public interface IEventPublisher
    {
        IObservable<TEvent> GetEvent<TEvent>() where TEvent : IEvent;
        void Publish<TEvent>(TEvent sampleEvent) where TEvent : IEvent;
    }
}
