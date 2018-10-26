namespace Lizzards.Data.CQRS.Queries
{
  using System.Linq;
  using Lizzards.Data.Domain;

  public sealed class QueryForAllBuilder<TPayload>
      : QueryBuilder<IQuery<IQueryable<TPayload>>>,
      IQueryBuilder<IQuery<IQueryable<TPayload>>>
    where TPayload : IAggregateRoot
  {
    protected override IQuery<IQueryable<TPayload>> NextBuildStep()
        => new Query<TPayload, IQueryable<TPayload>>(
            this.dataContext,
            this.logger,
            x => x.Collection(t => true));
  }
}
