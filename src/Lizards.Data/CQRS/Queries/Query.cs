using System;
using System.Threading.Tasks;
using NLog;
using Lizards.Data.CQRS.DataAccess;
using Lizards.Data.Domain;

namespace Lizards.Data.CQRS.Queries
{
    public sealed class Query<TPayload, TResult> : IAsyncQuery<TResult>
            where TPayload : IAggregateRoot
    {
        private readonly IDataContext storageContext;
        private readonly ILogger logger;
        private readonly Func<IDataReader<TPayload>, Task<TResult>> execute;

        public Query(
            IDataContext storageContext,
            ILogger logger,
            Func<IDataReader<TPayload>, Task<TResult>> execute)
        {
            this.storageContext = storageContext;
            this.logger = logger;
            this.execute = execute;
        }

        public Task<TResult> Execute()
            => this.execute(this.Read());

        private IDataReader<TPayload> Read()
            => this.storageContext.GetReader<TPayload>();
    }
}