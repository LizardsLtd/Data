namespace Lizzards.Data.CQRS.Queries
{
  using Lizzards.Data.CQRS.DataAccess;
  using Microsoft.Extensions.Logging;

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
