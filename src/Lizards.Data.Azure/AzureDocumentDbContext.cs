namespace Lizzards.Data.Azure
{
  using System;
  using System.Threading.Tasks;
  using Lizzards.Data.CQRS.DataAccess;
  using Lizzards.Data.Domain;
  using Lizzards.Data.Events;
  using Microsoft.Azure.Documents.Client;
  using Microsoft.Extensions.Logging;

  public sealed class AzureDocumentDbContext : IDataContext
  {
    private readonly AzureDocumentDbOptions options;
    private readonly Lazy<DocumentClient> client;
    private readonly IEventBus eventBus;
    private readonly ILogger logger;
    private bool disposedValue;

    public AzureDocumentDbContext(AzureDocumentDbOptions options, IEventBus bus, ILoggerFactory loggerFactory)
    {
      this.options = options;
      this.client = new Lazy<DocumentClient>(this.options.GetDocumentClient);
      this.eventBus = bus;
      this.logger = loggerFactory.CreateLogger<IDataContext>();
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
      this.logger.LogInformation("AzureDocumentDb: Disposing");

      this.Dispose(true);
    }

    private async Task<(string, string)> GetConfig<T>()
    {
      var databaseConfiguration = this.options.GetDatabaseConfig<T>();

      await this.PublishExceptionWhenMissingArgument(databaseConfiguration.Item1, "DatabaseId");
      await this.PublishExceptionWhenMissingArgument(databaseConfiguration.Item2, "CollectionId");

      this.logger.LogDebug($"AzudeDocumentDB: Datebase: ${databaseConfiguration.Item1}");
      this.logger.LogDebug($"AzudeDocumentDB: CollectionId: ${databaseConfiguration.Item2}");

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
