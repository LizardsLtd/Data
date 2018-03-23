using System;

namespace Lizards.Data.Events
{
    /// <summary>
    /// Basic interface to identitfy for EventBus
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Gets contains unique identifier for every event.
        /// </summary>
        Guid EventId { get; }
    }
}