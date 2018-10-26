namespace Lizzards.Data.InMemory
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Lizzards.Data.CQRS.DataAccess;
  using Lizzards.Maybe;

  public sealed class InMemoryReader<T> : IDataReader<T>
  {
    private readonly List<T> list;

    public InMemoryReader(List<T> list) => this.list = list;

    public Task<IQueryable<T>> Collection(Func<T, bool> predicate)
            => Task.FromResult(this.list.Where(predicate).AsQueryable());

    public Task<Maybe<T>> Single(Func<T, bool> predicate, Func<IEnumerable<T>, T> reduce)
        => Task.FromResult<Maybe<T>>(reduce(this.list.Where(predicate)));
  }
}
