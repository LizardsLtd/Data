namespace Lizzards.Data.CQRS.Queries
{
  using System.Linq;

  public sealed class QueryForAllBuilder<TPayload>
        : QueryBuilder<IQuery<IQueryable<TPayload>>>
        , IQueryBuilder<IQuery<IQueryable<TPayload>>>
  {
    protected override IQuery<IQueryable<TPayload>> NextBuildStep()
        => new Query<TPayload, IQueryable<TPayload>>(
            this.dataContext,
            this.logger,
            x => x.Collection(t => true));
  }
}
