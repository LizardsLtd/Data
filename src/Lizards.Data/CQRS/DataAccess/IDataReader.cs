using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lizards.Data.Domain;
using Lizards.Maybe;

namespace Lizards.Data.CQRS.DataAccess
{
    public interface IDataReader<TSource>
        where TSource : IAggregateRoot
    {
        Task<IQueryable<TSource>> Collection(Func<TSource, bool> predicate);

        Task<Maybe<TSource>> Single(Func<TSource, bool> predicate, Func<IEnumerable<TSource>, TSource> reduce);
    }
}