using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NLog;
using Lizards.Data.CQRS.DataAccess;
using Lizards.Data.Domain;

namespace Lizards.Data.CQRS.Queries
{
    public sealed class QueryBySpecification<TPayload> : IAsyncQuery<IEnumerable<TPayload>>
        where TPayload : IAggregateRoot
    {
        private readonly IDataContext dataContext;
        private readonly ILogger logger;
        private readonly Expression<Func<TPayload, bool>> specification;

        public QueryBySpecification(
            IDataContext dataContext,
            ILogger logger,
            Expression<Func<TPayload, bool>> specification)
        {
            this.dataContext = dataContext;
            this.logger = logger;
            this.specification = specification;
        }

        public Task<IEnumerable<TPayload>> Execute()
            => new QueryBySpecificationBuilder<TPayload>()
                .WithDataContext(this.dataContext)
                .WithLogger(this.logger)
                .WithSpecification(this.specification)
                .Execute();
    }
}