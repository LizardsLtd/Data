using Microsoft.Extensions.Logging;

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
        => this.logger.LogError(@event.Exception, @event.Message);
  }
}
