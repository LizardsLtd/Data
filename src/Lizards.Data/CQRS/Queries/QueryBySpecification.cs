namespace Lizzards.Data.CQRS.Queries
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
  using System.Threading.Tasks;
  using Lizzards.Data.CQRS.DataAccess;
  using Microsoft.Extensions.Logging;

  public sealed class QueryBySpecification<TPayload> : IQuery<IEnumerable<TPayload>>
  {
    private readonly IDataContext dataContext;
    private readonly ILogger logger;
    private readonly Expression<Func<TPayload, bool>> specification;

    public QueryBySpecification(
        IDataContext dataContext,
        ILogger logger,
        Expression<Func<TPayload, bool>> specification)
    {
      this.dataContext = dataContext;
      this.logger = logger;
      this.specification = specification;
    }

    public Task<IEnumerable<TPayload>> Execute()
        => new QueryBySpecificationBuilder<TPayload>()
            .WithDataContext(this.dataContext)
            .WithLogger(this.logger)
            .WithSpecification(this.specification)
            .Execute();
  }
}
