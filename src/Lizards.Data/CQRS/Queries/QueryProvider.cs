using Microsoft.Extensions.Logging;
using Lizzards.Data.CQRS.DataAccess;

namespace Lizzards.Data.CQRS.Queries
{
    public abstract class QueryProvider<TResult>
    {
        public QueryProvider(IDataContext dataContext, ILogger logger)
        {
            this.DataContext = dataContext;
            this.Logger = logger;
        }

        protected ILogger Logger { get; }

        protected IDataContext DataContext { get; }
    }
}