namespace Lizzards.Data.CQRS.Queries
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;
  using Lizzards.Data.CQRS.DataAccess;
  using Lizzards.Data.Domain;
  using Lizzards.Maybe;

  public sealed class QueryByIdBuilder<TPayload>
        : QueryBuilder<IWithId<IQuery<Maybe<TPayload>>>>,
        IWithId<IQuery<Maybe<TPayload>>>
            where TPayload : IAggregateRoot
  {
    public IQuery<Maybe<TPayload>> WithId(Guid id)
        => new Query<TPayload, Maybe<TPayload>>(
            this.dataContext,
            this.logger,
            reader => this.Execute(reader, id));

    protected override IWithId<IQuery<Maybe<TPayload>>> NextBuildStep() => this;

    private Task<Maybe<TPayload>> Execute(IDataReader<TPayload> reader, Guid id)
        => reader.Single(x => x.Id.Equals(id), items => items.SingleOrDefault());

    private Maybe<TPayload> SingleOrDefault(IQueryable<TPayload> items, Guid id)
    {
      var result = items.SingleOrDefault(x => x.Id.Equals(id));
      return (Maybe<TPayload>)result;
    }
  }
}
