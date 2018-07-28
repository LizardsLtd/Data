using System;
using System.Threading.Tasks;
using NLog;
using Lizzards.Data.CQRS.DataAccess;
using Lizzards.Data.Domain;
using Lizards.Maybe;

namespace Lizzards.Data.CQRS.Queries
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