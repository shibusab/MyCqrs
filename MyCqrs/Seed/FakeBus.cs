using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyCqrs.Seed
{
    public class FakeBus : ICommandSender, IEventPublisher
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _routes = new Dictionary<Type, List<Action<IMessage>>>();

        public void RegisterHandle<T>(Action<T> handler) where T: IMessage
        {
            List<Action<IMessage>> handlers;

            if (!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IMessage>>();
                _routes.Add(typeof(T), handlers);
            }
            handlers.Add((x => handler((T)x)));
        }

        public void Send<T>(T command) where T:Command
        {
            List<Action<IMessage>> handlers;

            if (!_routes.TryGetValue(typeof(T),out handlers))
            {
                throw new InvalidOperationException("No Handlers Registered");
            }
            else
            {
                if (handlers.Count != 1)
                {
                    throw new InvalidOperationException("Cannot Send More than one Handler");
                }

                handlers[0](command);
            }
        }

        public void Publish<T>(T @event) where T : Event
        {
            List<Action<IMessage>> handlers;

            if (!_routes.TryGetValue(@event.GetType(), out handlers))
            { return; }

            foreach(var handler in handlers)
            {
                var handler1 = handler;
                ThreadPool.QueueUserWorkItem(x => handler1(@event));
            }
        }
    }
}
