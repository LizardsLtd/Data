namespace Lizzards.Data.InMemory
{
  using System.Collections.Generic;
  using Lizzards.Data.CQRS.DataAccess;
  using Lizzards.Data.Domain;

  public sealed class InMemoryDataContext : IDataContext
  {
    private readonly IDictionary<string, object> items;

    public InMemoryDataContext() : this(new Dictionary<string, object>())
    {
    }

    public InMemoryDataContext(IDictionary<string, object> data) => items = data;

    public void Dispose()
    {
    }

    public IDataReader<TPayload> GetReader<TPayload>()
        where TPayload : IAggregateRoot
      => new InMemoryReader<TPayload>(GetItemsCollection<TPayload>());

    public IDataWriter<TPayload> GetWriter<TPayload>()
        where TPayload : IAggregateRoot
      => new InMemoryWriter<TPayload>(GetItemsCollection<TPayload>());

    public List<T> GetItemsCollection<T>()
    {
      var key = CreateKey<T>();

      CreateCollectionIfKeyNotExist<T>(key);

      var result = items[key] as List<T>;

      return result;
    }

    private void CreateCollectionIfKeyNotExist<T>(string key)
    {
      if (!items.ContainsKey(key))
      {
        items[key] = new List<T>();
      }
    }

    private string CreateKey<T>() => typeof(T).Name;
  }
}
