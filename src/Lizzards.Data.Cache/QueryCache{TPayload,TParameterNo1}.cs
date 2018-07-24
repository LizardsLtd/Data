namespace Data.Cache
{
    using Lizards.Data.CQRS;

    public sealed class QueryCache<TPayload, TParameterNo1> : IQuery<TPayload, TParameterNo1>
    {
        private readonly IQuery<TPayload, TParameterNo1> internalQuery;
        private readonly ICache cache;

        public QueryCache(IQuery<TPayload, TParameterNo1> internalQuery, ICache cache)
        {
            this.internalQuery = internalQuery;
            this.cache = cache;
        }

        public TPayload Execute(TParameterNo1 parameterNo1)
        {
            string key = this.CreateKey(parameterNo1);
            if (this.cache.ContainsKey(key))
            {
                return this.cache.GetValue<TPayload>(key);
            }

            var value = this.internalQuery.Execute(parameterNo1);
            this.cache.StoreValue(key, value);

            return value;
        }

        private string CreateKey(TParameterNo1 parameterNo1)
        {
            return $"{ this.internalQuery.GetType().FullName}::{parameterNo1}";
        }
    }
}