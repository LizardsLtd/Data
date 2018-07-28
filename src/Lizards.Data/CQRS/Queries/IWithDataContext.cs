using Lizzards.Data.CQRS.DataAccess;

namespace Lizzards.Data.CQRS.Queries
{
    public interface IWithDataContext<TResult>
    {
        TResult WithDataContext(IDataContext dataContext);
    }
}