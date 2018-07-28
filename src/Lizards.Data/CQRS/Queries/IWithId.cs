using System;

namespace Lizzards.Data.CQRS.Queries
{
    public interface IWithId<TResult>
    {
        TResult WithId(Guid id);
    }
}