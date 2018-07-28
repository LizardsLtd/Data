using System;
using System.Linq;
using System.Threading.Tasks;
using Lizzards.Data.CQRS.DataAccess;
using Lizzards.Data.Domain;
using Lizzards.Maybe;

namespace Lizzards.Data.CQRS.Queries
{
    public sealed class QueryByIdBuilder<TPayload>
        : QueryBuilder<IWithId<IAsyncQuery<Maybe<TPayload>>>>,
        IWithId<IAsyncQuery<Maybe<TPayload>>>
            where TPayload : IAggregateRoot
    {
        public IAsyncQuery<Maybe<TPayload>> WithId(Guid id)
            => new Query<TPayload, Maybe<TPayload>>(
                this.dataContext,
                this.logger,
                reader => this.Execute(reader, id));

        protected override IWithId<IAsyncQuery<Maybe<TPayload>>> NextBuildStep() => this;

        private Task<Maybe<TPayload>> Execute(IDataReader<TPayload> reader, Guid id)
            => reader.Single(x => x.Id.Equals(id), items => items.SingleOrDefault());

        private Maybe<TPayload> SingleOrDefault(IQueryable<TPayload> items, Guid id)
        {
            var result = items.SingleOrDefault(x => x.Id.Equals(id));
            return (Maybe<TPayload>)result;
        }
    }
}