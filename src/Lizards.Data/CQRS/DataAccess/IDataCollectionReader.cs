using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lizzards.Data.Domain;

namespace Lizzards.Data.CQRS.DataAccess
{
    public interface IDataCollectionReader<TSource> where TSource : IAggregateRoot
    {
        Task<IEnumerable<TSource>> Collection(Func<TSource, bool> predicate);
    }
}