namespace Lizzards.Data.Azure
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Lizzards.Data.CQRS.DataAccess;
  using Microsoft.Azure.Documents.Client;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  public sealed class AzureDocumentDbContextInitialiser : IDataContextInitialiser
  {
    private readonly DocumentClient client;
    private readonly ILogger logger;
    private readonly IEnumerable<AzureDatabaseCollection> databases;
    private bool disposedValue;

    public AzureDocumentDbContextInitialiser(IOptions<AzureDocumentDbOptions> options, ILogger logger)
    {
      this.client = options.Value.GetDocumentClient();
      this.databases = options.Value.GetDatabasesCollections();
      this.logger = logger;
    }

    public void Dispose()
    {
      this.logger.LogInformation("AzureDocumentDb: Disposing");

      this.Dispose(true);
    }

    public Task Initialise()
        => Task.Run(()
            => this.databases
                .ToList()
                .ForEach(azureDb => azureDb.CreateDatabaseWithCollection(this.client)));

    private void Dispose(bool disposing)
    {
      if (!this.disposedValue)
      {
        lock (this.client)
        {
          if (!this.disposedValue)
          {
            if (disposing)
            {
              this.client.Dispose();
            }

            this.disposedValue = true;
          }
        }
      }
    }
  }
}
