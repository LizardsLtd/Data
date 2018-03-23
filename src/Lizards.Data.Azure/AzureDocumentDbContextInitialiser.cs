using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using NLog;
using Lizards.Data.CQRS.DataAccess;

namespace Lizards.Data.Azure
{
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
            this.logger.Info("AzureDocumentDb: Disposing");

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