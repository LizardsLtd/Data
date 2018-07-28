namespace Lizzards.Data.Azure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using Lizzards.Data.Domain;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Configuration;
    using NLog;

    public sealed class AzureDocumentDbOptions
    {
        private readonly Logger logger;

        private string endpoint;

        private string authKey;

        private IEnumerable<AzureDatabaseCollection> databases;

        public AzureDocumentDbOptions()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        private Uri EndpointUri => new Uri(this.endpoint);

        private SecureString AuthenticationKey
            => this.authKey
                .ToCharArray()
                .Aggregate(
                    new SecureString(),
                    (secureString, character) =>
                    {
                        secureString.AppendChar(character);
                        return secureString;
                    });

        public static void FromConfiguration(IConfiguration config, AzureDocumentDbOptions options)
        {
            options.databases = config
                .GetSection("Databases")
                .GetChildren()
                .Select(x => new
                {
                    Name = x["Name"],
                    AggregateRoot = x["Type"],
                    Collection = x["Collection"],
                })
                .Select(x => new AzureDatabaseCollection(x.Name, x.AggregateRoot, x.Collection))
                .ToArray();
            options.endpoint = config["ConnectionString:AccountEndpoint"];
            options.authKey = config["ConnectionString:AccountKey"];
        }

        internal (string, string) GetDatabaseConfig<T>()
            where T : IAggregateRoot
            => this.databases
                .Where(x => x.IsMatchForType<T>())
                .Select(x => (x.DatabaseId, x.Collection))
                .FirstOrDefault();

        internal IEnumerable<AzureDatabaseCollection> GetDatabasesCollections() => this.databases;

        internal DocumentClient GetDocumentClient() => new DocumentClient(this.EndpointUri, this.authKey);
    }
}