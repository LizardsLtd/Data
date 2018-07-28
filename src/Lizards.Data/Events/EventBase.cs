namespace Lizzards.Data.Events
{
    using System;

    public abstract class EventBase : IEvent
    {
        public EventBase()
            => this.EventId = Guid.NewGuid();

        public Guid EventId { get; }
    }
}