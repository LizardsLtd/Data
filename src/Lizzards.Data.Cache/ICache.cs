namespace Data.Cache
{
    public interface ICache
    {
        bool ContainsKey(string key);

        T GetValue<T>(string key);

        void StoreValue<TPayload>(string key, TPayload value);
    }
}