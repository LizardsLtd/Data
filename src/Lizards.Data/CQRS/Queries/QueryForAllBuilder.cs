using System.Linq;
using Lizzards.Data.Domain;

namespace Lizzards.Data.CQRS.Queries
{
    public sealed class QueryForAllBuilder<TPayload>
        : QueryBuilder<IAsyncQuery<IQueryable<TPayload>>>
        , IQueryBuilder<IAsyncQuery<IQueryable<TPayload>>>
            where TPayload : IAggregateRoot
    {
        protected override IAsyncQuery<IQueryable<TPayload>> NextBuildStep()
            => new Query<TPayload, IQueryable<TPayload>>(
                this.dataContext,
                this.logger,
                x => x.Collection(t => true));
    }
}