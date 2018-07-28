using NLog;

namespace Lizzards.Data.Events
{
    public sealed class LoggingEventListener
    {
        private readonly ILogger logger;

        public LoggingEventListener(IEventBus bus, ILogger logger)
        {
            this.logger = logger;

            bus.Subscribe<ExceptionEvent>(this.LogException);
        }

        private void LogException(ExceptionEvent @event)
            => this.logger.Error(@event.Exception, @event.Message);
    }
}