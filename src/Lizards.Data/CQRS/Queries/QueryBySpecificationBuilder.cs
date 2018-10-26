namespace Lizzards.Data.CQRS.Queries
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Threading.Tasks;
  using Lizzards.Data.CQRS.DataAccess;
  using Lizzards.Data.Domain;

  public sealed class QueryBySpecificationBuilder<TPayload>
      : QueryBuilder<IWithSpecification<IQuery<IEnumerable<TPayload>>, TPayload>>,
      IWithSpecification<IQuery<IEnumerable<TPayload>>, TPayload>
    where TPayload : IAggregateRoot
  {
    public IQuery<IEnumerable<TPayload>> WithSpecification(Expression<Func<TPayload, bool>> specification)
    {
      return new Query<TPayload, IEnumerable<TPayload>>(
        dataContext,
        logger,
        reader => Execute(reader, specification));
    }

    protected override IWithSpecification<IQuery<IEnumerable<TPayload>>, TPayload> NextBuildStep()
    {
      return this;
    }

    private async Task<IEnumerable<TPayload>> Execute(
      IDataReader<TPayload> reader,
      Expression<Func<TPayload, bool>> specification)
    {
      return (await reader.Collection(specification.Compile())).ToArray();
    }
  }
}
