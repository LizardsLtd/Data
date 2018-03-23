using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NLog;
using Lizards.Data.CQRS.DataAccess;
using Lizards.Data.CQRS.Queries;
using Lizards.Data.InMemory;
using Lizards.Data.Tests.Mocks;
using Xunit;

namespace Lizards.Data.Tests.CQRS.Queries
{
    public sealed class QueryByIdTests
    {
        private readonly IDataContext context;
        private readonly Guid id;

        public QueryByIdTests()
        {
            this.id = Guid.NewGuid();
            var storage = new Dictionary<string, object>
            {
                ["SimpleAggregateRoot"] = new List<SimpleAggregateRoot>
                {
                    new SimpleAggregateRoot(id),
                },
            };
            this.context = new InMemoryDataContext(storage);
        }

        [Fact]
        public async Task QueringForExistingEntity()
        {
            var query = new QueryById<SimpleAggregateRoot>(
                    this.context,
                    LogManager.GetCurrentClassLogger(),
                    this.id);

            var result = await query.Execute();

            result.IsSome.Should().Be(true);
            result.Value.Id.Should().Be(this.id);
        }
    }
}