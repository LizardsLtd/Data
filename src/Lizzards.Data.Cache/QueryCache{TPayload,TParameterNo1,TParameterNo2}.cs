namespace Data.Cache
{
    using Lizards.Data.CQRS;

    public sealed class QueryCache<TPayload, TParameterNo1, TParameterNo2> : IQuery<TPayload, TParameterNo1, TParameterNo2>
    {
        private readonly IQuery<TPayload, TParameterNo1, TParameterNo2> internalQuery;
        private readonly ICache cache;

        public QueryCache(IQuery<TPayload, TParameterNo1, TParameterNo2> internalQuery, ICache cache)
        {
            this.internalQuery = internalQuery;
            this.cache = cache;
        }

        public TPayload Execute(TParameterNo1 parameterNo1, TParameterNo2 parameterNo2)
        {
            string key = this.CreateKey(parameterNo1, parameterNo2);
            if (this.cache.ContainsKey(key))
            {
                return this.cache.GetValue<TPayload>(key);
            }

            var value = this.internalQuery.Execute(parameterNo1, parameterNo2);
            this.cache.StoreValue(key, value);

            return value;
        }

        private string CreateKey(TParameterNo1 parameterNo1, TParameterNo2 parameterNo2)
        {
            return $"{ this.internalQuery.GetType().FullName}::{parameterNo1}::{parameterNo2}";
        }
    }
}