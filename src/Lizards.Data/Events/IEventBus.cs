using System;
using System.Threading.Tasks;

namespace Lizzards.Data.Events
{
    /// <summary>
    /// Implementation of publisher/subscriber pattern used to handle no modificing events.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Allows to publish the event to all subscribers by event type
        /// </summary>
        /// <typeparam name="T">Type of event you want to publish</typeparam>
        /// <param name="event">Event to be published</param>
        /// <returns>Asynchronus void</returns>
        Task Publish<T>(T @event)
            where T : IEvent;

        /// <summary>
        /// Allows to subscribe to the envet
        /// </summary>
        /// <typeparam name="T">Type of the event</typeparam>
        /// <param name="subscriber">Action to be taken when event occurs</param>
        /// <returns>Asynchronus void</returns>
        Task Subscribe<T>(Action<T> subscriber)
            where T : IEvent;
    }
}