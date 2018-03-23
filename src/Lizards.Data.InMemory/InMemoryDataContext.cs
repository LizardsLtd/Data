using System.Collections.Generic;
using Lizards.Data.CQRS.DataAccess;
using Lizards.Data.Domain;

namespace Lizards.Data.InMemory
{
    public sealed class InMemoryDataContext : IDataContext
    {
        private readonly IDictionary<string, object> items;

        public InMemoryDataContext() : this(new Dictionary<string, object>())
        {
        }

        public InMemoryDataContext(IDictionary<string, object> data)
        {
            this.items = data;
        }

        public void Dispose()
        {
        }

        public IDataReader<T> GetReader<T>()
                where T : IAggregateRoot
            => new InMemoryReader<T>(this.GetItemsCollection<T>());

        public IDataWriter<T> GetWriter<T>()
                where T : IAggregateRoot
            => new InMemoryWriter<T>(this.GetItemsCollection<T>());

        public List<T> GetItemsCollection<T>()
            where T : IAggregateRoot
        {
            var key = this.CreateKey<T>();

            this.CreateCollectionIfKeyNotExist<T>(key);

            List<T> result = this.items[key] as List<T>;

            return result;
        }

        private void CreateCollectionIfKeyNotExist<T>(string key)
        {
            if (!this.items.ContainsKey(key))
            {
                this.items[key] = new List<T>();
            }
        }

        private string CreateKey<T>()
                where T : IAggregateRoot
            => typeof(T).Name;
    }
}