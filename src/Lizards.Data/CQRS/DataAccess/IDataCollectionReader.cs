using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lizards.Data.Domain;

namespace Lizards.Data.CQRS.DataAccess
{
    public interface IDataCollectionReader<TSource> where TSource : IAggregateRoot
    {
        Task<IEnumerable<TSource>> Collection(Func<TSource, bool> predicate);
    }
}