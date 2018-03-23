namespace Lizards.Data.InMemory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Lizards.Data.CQRS.DataAccess;
    using Lizards.Data.Domain;
    using Lizards.Maybe;

    public sealed class InMemoryReader<T> : IDataReader<T>
        where T : IAggregateRoot
    {
        private List<T> list;

        public InMemoryReader(List<T> list)
        {
            this.list = list;
        }

        public Task<IQueryable<T>> Collection(Func<T, bool> predicate)
            => Task.FromResult(this.list.Where(predicate).AsQueryable());

        public Task<Maybe<T>> Single(Func<T, bool> predicate, Func<IEnumerable<T>, T> reduce)
            => Task.FromResult<Maybe<T>>(reduce(this.list.Where(predicate)));
    }
}