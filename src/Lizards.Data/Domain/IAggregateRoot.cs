using System;

namespace Lizzards.Data.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}