using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace MemoryCache.Services
{
    public class CacheManager : ICacheManager
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly IMemoryCache _cache;

        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public T Get<T>(string key, Func<T> acquire, int cacheTime)
        {
            // item already is in cache, so return it
            if (_cache.TryGetValue(key, out T value))
            {
                return value;
            }
            // or create it using passed function
            var result = acquire();

            Set(key, result, cacheTime);

            return result;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int cacheTime)
        {
            // item already is in cache, so return it
            if (_cache.TryGetValue(key, out T value))
            {
                return value;
            }
            // or create it using passed function
            var result = await acquire();

            Set(key, result, cacheTime);

            return result;
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (data != null)
            {
                _cache.Set(key, data, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheTime)
                }.AddExpirationToken(new CancellationChangeToken(_cancellationTokenSource.Token)));
            }
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
