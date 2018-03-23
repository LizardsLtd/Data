using System;
using System.Threading.Tasks;
using NLog;
using Lizards.Data.CQRS.DataAccess;
using Lizards.Data.Domain;
using Lizards.Maybe;

namespace Lizards.Data.CQRS.Queries
{
    public sealed class QueryById<TPayload> : IAsyncQuery<Maybe<TPayload>>
        where TPayload : IAggregateRoot
    {
        private readonly IDataContext dataContext;
        private readonly ILogger logger;
        private readonly Guid id;

        public QueryById(IDataContext dataContext, ILogger logger,  Guid id)
        {
            this.dataContext = dataContext;
            this.logger = logger;
            this.id = id;
        }

        public Task<Maybe<TPayload>> Execute()
            => new QueryByIdBuilder<TPayload>()
                .WithDataContext(this.dataContext)
                .WithLogger(this.logger)
                .WithId(this.id)
                .Execute();
    }
}