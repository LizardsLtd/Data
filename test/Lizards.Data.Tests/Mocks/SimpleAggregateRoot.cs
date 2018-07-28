using System;
using Lizzards.Data.Domain;

namespace Lizzards.Data.Tests.Mocks
{
    internal sealed class SimpleAggregateRoot : IAggregateRoot
    {
        public SimpleAggregateRoot(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}