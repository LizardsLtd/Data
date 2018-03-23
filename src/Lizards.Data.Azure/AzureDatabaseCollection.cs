using System;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Lizards.Data.Azure
{
    internal sealed class AzureDatabaseCollection
    {
        internal AzureDatabaseCollection(string databaseId, string aggregateRoot, string collection)
        {
            this.DatabaseId = databaseId;
            this.AggregateRoot = aggregateRoot;
            this.Collection = collection;
        }

        public string DatabaseId { get; }

        public string AggregateRoot { get; }

        public string Collection { get; }

        internal bool IsMatchForType<T>() => AggregateRoot.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase);

        internal void CreateDatabaseWithCollection(DocumentClient client)
            => new AzureDatabaseCreator(client)
                .EnsureDatabseExist(this.CreateDatabase())
                .EnsureCollectionExists(this.Collection);

        private Database CreateDatabase()
            => new Database
            {
                Id = this.DatabaseId,
            };
    }
}