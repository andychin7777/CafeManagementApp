namespace CafeManagementApp.Shared
{
    public class MappingCacheHelper<T, T2>
    {
        private readonly Dictionary<string, object> _cache;
        private string _key;

        public MappingCacheHelper(string idString, ref Dictionary<string, object> cache)
        {
            _key = $"{typeof(T).FullName}_{idString}";            
            if (cache == null)
            {
                //pass in via ref cache dictionary will have the cache updated to new dictionary from ref
                cache = new Dictionary<string, object>();
            }
            _cache = cache;
        }

        public bool TryGetExistingEntityElseMap(Func<T2> mapObject, out T2 mappedObject)
        {
            mappedObject = default;

            if (_cache.TryGetValue(_key, out var existingEntity))
            {
                mappedObject = (T2)existingEntity;
                return true;
            }

            //map the object accordingly
            mappedObject = mapObject();
            AddToCache(mappedObject);

            return false;
        }

        public Dictionary<string, object> GetCacheDictionary()
        {
            return _cache;
        }

        private void AddToCache(T2 entity)
        {
            if (!_cache.ContainsKey(_key))
            {
                _cache.Add(_key, entity);
            }
        }
    }
}
