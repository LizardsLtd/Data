namespace Lizzards.Data.Azure
{
  using System.Threading.Tasks;
  using Lizzards.Data.CQRS.DataAccess;
  using Lizzards.Data.Domain;
  using Microsoft.Azure.Documents;
  using Microsoft.Azure.Documents.Client;
  using Microsoft.Extensions.Logging;

  internal class AzureDocumentDbDataWriter<TPayload> : IDataWriter<TPayload>
    where TPayload : IAggregateRoot
  {
    private readonly DocumentClient client;
    private readonly string collectionUri;
    private readonly string databaseId;
    private readonly ILogger logger;

    internal AzureDocumentDbDataWriter(DocumentClient client, string databaseId, string collectionUri, ILogger logger)
    {
      this.client = client;
      this.databaseId = databaseId;
      this.collectionUri = collectionUri;
      this.logger = logger;
    }

    public async Task InsertNew(TPayload item)
        => await this.InsertDocument(item);

    public async Task UpdateExisting(TPayload item)
        => await this.InsertDocument(item, await this.QuertyForETag(item.Id));

    private async Task<string> QuertyForETag(object itemId)
    {
      var documentUri = UriFactory.CreateDocumentUri(
          this.databaseId,
          this.collectionUri,
          itemId.ToString());

      var document = await this.client.ReadDocumentAsync(documentUri);

      return document?.Resource?.ETag;
    }

    private async Task InsertDocument(TPayload item, string etag = "")
    {
      try
      {
        var requestOptions = new RequestOptions();

        if (!string.IsNullOrEmpty(etag))
        {
          requestOptions.AccessCondition = new AccessCondition
          {
            Condition = etag,
            Type = AccessConditionType.IfMatch,
          };
        }

        await this.client.UpsertDocumentAsync(
              UriFactory.CreateDocumentCollectionUri(this.databaseId, this.collectionUri)
              , item
              , requestOptions);
      }
      catch (DocumentClientException exp)
      {
        this.logger.LogError(exp.Message);
      }
    }
  }
}
