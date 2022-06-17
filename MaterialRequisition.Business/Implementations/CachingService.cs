using MaterialRequisition.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace MaterialRequisition.Business.Implementations
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _memoryCache;

        public CachingService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T RetrieveCacheEntry<T>(string cacheKey)
        {
            _memoryCache.TryGetValue(cacheKey, out T result);
            return result;
        }

        public void SetCacheEntry(string cacheKey, object entry, int? expiryInSeconds = null, MemoryCacheEntryOptions cacheOptions = null)
        {
            if(cacheOptions == null)
            {
                cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(expiryInSeconds ?? 10));
            }
            _memoryCache.Set(cacheKey, entry, cacheOptions);
        }
    }
}
