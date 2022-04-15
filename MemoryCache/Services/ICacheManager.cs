namespace MemoryCache.Services
{
    public interface ICacheManager
    {
        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it.
        /// </summary>
        /// <typeparam name="T">Type of cached item.</typeparam>
        /// <param name="key">Cache key.</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet.</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache;</param>
        /// <returns>The cached value associated with the specified key</returns>
        T Get<T>(string key, Func<T> acquire, int cacheTime);

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it.
        /// </summary>
        /// <typeparam name="T">Type of cached item.</typeparam>
        /// <param name="key">Cache key.</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet.</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache;</param>
        /// <returns>The cached value associated with the specified key.</returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int cacheTime);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">Key of cached item.</param>
        /// <param name="data">Value for caching.</param>
        /// <param name="cacheTime">Cache time in minutes.</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// Removes the value with the specified key from the cache.
        /// </summary>
        /// <param name="key">Key of cached item.</param>
        void Remove(string key);
    }
}
