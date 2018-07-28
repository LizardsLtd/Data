namespace Lizzards.Data.CQRS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Lizzards.Data.Domain;
    using Lizzards.Maybe;

    public interface IDataReader<TSource>
        where TSource : IAggregateRoot
    {
        Task<IQueryable<TSource>> Collection(Func<TSource, bool> predicate);

        Task<Maybe<TSource>> Single(Func<TSource, bool> predicate, Func<IEnumerable<TSource>, TSource> reduce);
    }
}