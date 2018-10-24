using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lizzards.Data.CQRS.DataAccess;
using Lizzards.Data.Domain;
using Microsoft.Extensions.Logging;

namespace Lizzards.Data.CQRS.Queries
{
  public sealed class QueryForAll<TPayload> : IAsyncQuery<IEnumerable<TPayload>>
            where TPayload : IAggregateRoot
  {
    private readonly IDataContext dataContext;
    private readonly ILogger logger;

    public QueryForAll(IDataContext dataContext, ILogger logger)
    {
      this.dataContext = dataContext;
      this.logger = logger;
    }

    public async Task<IEnumerable<TPayload>> Execute()
        => (await this.ExecuteQuery()).ToArray();

    private async Task<IQueryable<TPayload>> ExecuteQuery()
        => await new QueryForAllBuilder<TPayload>()
            .WithDataContext(this.dataContext)
            .WithLogger(this.logger)
            .Execute();
  }
}
