namespace Lizzards.Data.InMemory
{
  using System.Collections.Generic;
  using Lizzards.Data.CQRS.DataAccess;

  public sealed class InMemoryDataContext : IDataContext
  {
    private readonly IDictionary<string, object> items;

    public InMemoryDataContext() : this(new Dictionary<string, object>())
    {
    }

    public InMemoryDataContext(IDictionary<string, object> data) => this.items = data;

    public void Dispose()
    {
    }

    public IDataReader<T> GetReader<T>()
        => new InMemoryReader<T>(this.GetItemsCollection<T>());

    public IDataWriter<T> GetWriter<T>()
        => new InMemoryWriter<T>(this.GetItemsCollection<T>());

    public List<T> GetItemsCollection<T>()
    {
      var key = this.CreateKey<T>();

      this.CreateCollectionIfKeyNotExist<T>(key);

      var result = this.items[key] as List<T>;

      return result;
    }

    private void CreateCollectionIfKeyNotExist<T>(string key)
    {
      if (!this.items.ContainsKey(key))
      {
        this.items[key] = new List<T>();
      }
    }

    private string CreateKey<T>() => typeof(T).Name;
  }
}
