using System.Collections.Generic;
using System.Linq;

namespace Lizzards.Data.Azure
{
    public static class Materializer
    {
        public static IEnumerable<T> Materialize<T>(this IQueryable<T> items)
            => items.ToArray();
    }
}