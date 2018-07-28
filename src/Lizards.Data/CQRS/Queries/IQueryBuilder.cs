namespace Lizzards.Data.CQRS.Queries
{
    public interface IQueryBuilder<TResult> : IWithDataContext<IWithLogger<TResult>>, IWithLogger<TResult>
    { }
}