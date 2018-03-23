using System;

namespace Lizards.Data.CQRS.Queries
{
    public interface IWithId<TResult>
    {
        TResult WithId(Guid id);
    }
}