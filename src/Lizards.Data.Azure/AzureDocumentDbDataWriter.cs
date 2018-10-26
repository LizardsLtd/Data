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

    public AzureDocumentDbDataWriter(DocumentClient client, string databaseId, string collectionUri, ILogger logger)
    {
      this.client = client;
      this.databaseId = databaseId;
      this.collectionUri = collectionUri;
      this.logger = logger;
    }

    public async Task InsertNew(TPayload item)
        => await InsertDocument(item);

    public async Task UpdateExisting(TPayload item)
        => await InsertDocument(item, await QuertyForETag(item.Id));

    private async Task<string> QuertyForETag(object itemId)
    {
      var documentUri = UriFactory.CreateDocumentUri(
          databaseId,
          collectionUri,
          itemId.ToString());

      var document = await client.ReadDocumentAsync(documentUri);

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

        await client.UpsertDocumentAsync(
              UriFactory.CreateDocumentCollectionUri(databaseId, collectionUri)
              , item
              , requestOptions);
      }
      catch (DocumentClientException exp)
      {
        logger.LogError(exp.Message);
      }
    }
  }
}
