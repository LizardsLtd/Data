using Microsoft.Extensions.Logging;

namespace Lizzards.Data.CQRS.Queries
{
    public interface IWithLogger<TResult>
    {
        TResult WithLogger(ILogger logger);
    }
}