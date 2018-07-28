using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Lizzards.Data.Azure
{
    internal sealed class AzureDatabaseCreator
    {
        private readonly DocumentClient client;
        private Database database;

        public AzureDatabaseCreator(DocumentClient client)
        {
            this.client = client;
        }

        internal AzureDatabaseCreator EnsureDatabseExist(Database database)
        {
            if (this.DoesDatabaseHasToBeCreated(database))
            {
                this.CreateDatabase(database).Wait();
            }

            this.database = database;

            return this;
        }

        internal void EnsureCollectionExists(string collection)
        {
            var databaseLink = UriFactory.CreateDatabaseUri(this.database.Id);

            var documentCollection = new DocumentCollection
            {
                Id = collection,
            };

            if (this.DoesCollectionHasToBeCreated(databaseLink, documentCollection))
            {
                this.CreateCollection(databaseLink, documentCollection).Wait();
            }
        }

        private async Task CreateCollection(Uri databaseLink, DocumentCollection documentCollection)
            => await client.CreateDocumentCollectionAsync(
                databaseLink
                , documentCollection
                , new RequestOptions { OfferThroughput = 1000 });

        private async Task CreateDatabase(Database database)
            => await this.client.CreateDatabaseAsync(
                database
                , new RequestOptions { OfferThroughput = 1000 });

        private bool DoesDatabaseHasToBeCreated(Database database)
            => !this.DoesDatabaseExist(database);

        private bool DoesDatabaseExist(Database database)
            => this.client
                .CreateDatabaseQuery()
                .Where(db => db.Id == database.Id)
                .ToArray()
                .Any();

        private bool DoesCollectionHasToBeCreated(Uri databaseLink, DocumentCollection documentCollection)
            => !this.DoesCollectionExist(databaseLink, documentCollection);

        private bool DoesCollectionExist(Uri databaseLink, DocumentCollection documentCollection)
            => this.client
                .CreateDocumentCollectionQuery(databaseLink)
                .Where(col => col.Id == documentCollection.Id)
                .ToArray()
                .Any();
    }
}