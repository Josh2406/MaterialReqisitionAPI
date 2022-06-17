using Microsoft.Extensions.Caching.Memory;

namespace MaterialRequisition.Application.Interfaces
{
    public interface ICachingService
    {
        T RetrieveCacheEntry<T>(string cacheKey);
        void SetCacheEntry(string cacheKey, object entry, int? expiryInSeconds = null, MemoryCacheEntryOptions cacheOptions = null);
    }
}
