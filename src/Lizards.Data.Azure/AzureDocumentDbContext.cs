using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using NLog;
using Lizards.Data.CQRS.DataAccess;
using Lizards.Data.Domain;
using Lizards.Data.Events;

namespace Lizards.Data.Azure
{
    public sealed class AzureDocumentDbContext : IDataContext
    {
        private readonly AzureDocumentDbOptions options;
        private readonly Lazy<DocumentClient> client;
        private readonly IEventBus eventBus;
        private readonly ILogger logger;
        private bool disposedValue;

        public AzureDocumentDbContext(IOptions<AzureDocumentDbOptions> options, IEventBus bus, ILogger logger)
        {
            this.options = options.Value;
            this.client = new Lazy<DocumentClient>(this.options.GetDocumentClient);
            this.eventBus = bus;
            this.logger = logger;
        }

        private bool IsClientCreated => this.client?.IsValueCreated ?? false;

        public IDataReader<T> GetReader<T>()
            where T : IAggregateRoot
        {
            (var databaseId, var collectionId) = this.GetConfig<T>().Result;

            var collectionUri = this.GetCollectionUri(databaseId, collectionId);

            return new AzureDocumentDbDataReader<T>(
                this.client.Value,
                collectionUri,
                this.logger);
        }

        public IDataWriter<T> GetWriter<T>()
            where T : IAggregateRoot
        {
            (var databaseId, var collectionId) = this.GetConfig<T>().Result;

            return new AzureDocumentDbDataWriter<T>(
                this.client.Value,
                databaseId,
                collectionId,
                this.logger);
        }

        public void Dispose()
        {
            this.logger.Info("AzureDocumentDb: Disposing");

            this.Dispose(true);
        }

        private async Task<(string, string)> GetConfig<T>()
            where T : IAggregateRoot
        {
            var databaseConfiguration = this.options.GetDatabaseConfig<T>();

            await this.PublishExceptionWhenMissingArgument(databaseConfiguration.Item1, "DatabaseId");
            await this.PublishExceptionWhenMissingArgument(databaseConfiguration.Item2, "CollectionId");

            this.logger.Debug($"AzudeDocumentDB: Datebase: ${databaseConfiguration.Item1}");
            this.logger.Debug($"AzudeDocumentDB: CollectionId: ${databaseConfiguration.Item2}");

            return databaseConfiguration;
        }

        private async Task PublishExceptionWhenMissingArgument(string argument, string name)
        {
            if (string.IsNullOrEmpty(argument))
            {
                await this.eventBus.Publish(
                    new ExceptionEvent(
                        new ArgumentNullException("name is missing")));
            }
        }

        private Uri GetCollectionUri(string databaseId, string collectionId)
            => UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue && this.IsClientCreated)
            {
                lock (this.client)
                {
                    if (!this.disposedValue && this.IsClientCreated)
                    {
                        if (disposing)
                        {
                            this.client.Value.Dispose();
                        }

                        this.disposedValue = true;
                    }
                }
            }
        }
    }
}