namespace Data.Cache
{
    using Lizards.Data.CQRS;

    public sealed class QueryCache<TPayload> : IQuery<TPayload>
    {
        private readonly IQuery<TPayload> internalQuery;
        private readonly ICache cache;

        public QueryCache(IQuery<TPayload> internalQuery, ICache cache)
        {
            this.internalQuery = internalQuery;
            this.cache = cache;
        }

        public TPayload Execute()
        {
            var key = this.internalQuery.GetType().FullName;
            if (this.cache.ContainsKey(key))
            {
                return this.cache.GetValue<TPayload>(key);
            }

            var value = this.internalQuery.Execute();
            this.cache.StoreValue(key, value);

            return value;
        }
    }
}