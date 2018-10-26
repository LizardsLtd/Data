namespace Lizzards.Data.Azure
{
  using System.Collections.Generic;
  using System.Linq;

  public static class Materializer
  {
    public static IEnumerable<T> Materialize<T>(this IQueryable<T> items)
      => items.ToArray();
  }
}
