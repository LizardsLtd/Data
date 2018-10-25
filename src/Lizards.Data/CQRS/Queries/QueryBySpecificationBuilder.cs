using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lizzards.Data.CQRS.DataAccess;

namespace Lizzards.Data.CQRS.Queries
{
  public sealed class QueryBySpecificationBuilder<TPayload>
        : QueryBuilder<IWithSpecification<IQuery<IEnumerable<TPayload>>, TPayload>>
        , IWithSpecification<IQuery<IEnumerable<TPayload>>, TPayload>
  {
    public IQuery<IEnumerable<TPayload>> WithSpecification(Expression<Func<TPayload, bool>> specification)
        => new Query<TPayload, IEnumerable<TPayload>>(
            this.dataContext,
            this.logger,
            reader => this.Execute(reader, specification));

    protected override IWithSpecification<IQuery<IEnumerable<TPayload>>, TPayload> NextBuildStep() => this;

    private async Task<IEnumerable<TPayload>> Execute(
            IDataReader<TPayload> reader,
            Expression<Func<TPayload, bool>> specification)
        => (await reader.Collection(specification.Compile())).ToArray();
  }
}
