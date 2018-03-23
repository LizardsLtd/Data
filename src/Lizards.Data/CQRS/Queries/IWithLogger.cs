using NLog;

namespace Lizards.Data.CQRS.Queries
{
    public interface IWithLogger<TResult>
    {
        TResult WithLogger(ILogger logger);
    }
}