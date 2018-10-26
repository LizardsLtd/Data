namespace Lizzards.Data.InMemory
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Lizzards.Data.CQRS.DataAccess;

  internal class InMemoryWriter<T> : IDataWriter<T>
  {
    private readonly List<T> list;

    public InMemoryWriter(List<T> list) => this.list = list;

    public Task InsertNew(T item)
    {
      this.list.Add(item);
      return Task.CompletedTask;
    }

    public Task UpdateExisting(T item)
    {
      var selectedItem = this.list.First(x => x.Equals(item));

      this.list.Remove(selectedItem);

      return this.InsertNew(item);
    }
  }
}
