using System;
using Lizards.Data.Domain;

namespace Lizards.Data.Tests.Mocks
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