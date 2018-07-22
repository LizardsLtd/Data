namespace Lizards.Data.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class EventBus : IEventBus
    {
        private readonly List<(Type, object)> subscribers;

        public EventBus()
        {
            this.subscribers = new List<(Type, object)>();
        }

        public Task Publish<T>(T @event)
            where T : IEvent
        {
            var type = typeof(T);
            this.subscribers
                .Where(x => x.Item1 == type)
                .Select(x => x.Item2)
                .Cast<Action<T>>()
                .ToList()
                .ForEach(action => action(@event));

            return Task.CompletedTask;
        }

        public Task Subscribe<T>(Action<T> subscriber)
            where T : IEvent
        {
            this.subscribers.Add((typeof(T), subscriber));
            return Task.CompletedTask;
        }
    }
}