using Lizards.Data.CQRS.DataAccess;

namespace Lizards.Data.CQRS.Queries
{
    public interface IWithDataContext<TResult>
    {
        TResult WithDataContext(IDataContext dataContext);
    }
}