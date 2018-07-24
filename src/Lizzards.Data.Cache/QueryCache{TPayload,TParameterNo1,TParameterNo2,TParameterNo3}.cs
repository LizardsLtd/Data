namespace Data.Cache
{
    using Lizards.Data.CQRS;

    public sealed class QueryCache<TPayload, TParameterNo1, TParameterNo2, TParameterNo3> : IQuery<TPayload, TParameterNo1, TParameterNo2, TParameterNo3>
    {
        private readonly IQuery<TPayload, TParameterNo1, TParameterNo2, TParameterNo3> internalQuery;
        private readonly ICache cache;

        public QueryCache(IQuery<TPayload, TParameterNo1, TParameterNo2, TParameterNo3> internalQuery, ICache cache)
        {
            this.internalQuery = internalQuery;
            this.cache = cache;
        }

        public TPayload Execute(TParameterNo1 parameterNo1, TParameterNo2 parameterNo2, TParameterNo3 parameterNo3)
        {
            string key = this.CreateKey(parameterNo1, parameterNo2, parameterNo3);
            if (this.cache.ContainsKey(key))
            {
                return this.cache.GetValue<TPayload>(key);
            }

            var value = this.internalQuery.Execute(parameterNo1, parameterNo2, parameterNo3);
            this.cache.StoreValue(key, value);

            return value;
        }

        private string CreateKey(TParameterNo1 parameterNo1, TParameterNo2 parameterNo2, TParameterNo3 parameterNo3)
        {
            return $"{ this.internalQuery.GetType().FullName}::{parameterNo1}::{parameterNo2}::{parameterNo3}";
        }
    }
}