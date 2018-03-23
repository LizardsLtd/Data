namespace Lizards.Data.Tests.CQRS.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Lizards.Data.CQRS.DataAccess;
    using Lizards.Data.CQRS.Queries;
    using Lizards.Data.InMemory;
    using Lizards.Data.Tests.Mocks;
    using NLog;
    using Xunit;

    public sealed class QueryForAllTests
    {
        private readonly IDataContext context;
        private readonly Guid id;

        public QueryForAllTests()
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
            var query = new QueryForAll<SimpleAggregateRoot>(
                    this.context,
                    LogManager.GetCurrentClassLogger());

            var result = await query.Execute();

            result.Count().Should().Be(1);
        }
    }
}