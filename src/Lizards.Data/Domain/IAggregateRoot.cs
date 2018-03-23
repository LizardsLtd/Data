using System;

namespace Lizards.Data.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}