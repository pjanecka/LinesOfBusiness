using Microsoft.Extensions.Caching.Memory;

namespace LinesOfBusiness.Repositories
{
    public class CachedGwpRepository : IGwpRepository
    {
        private readonly IGwpRepository gwpRepository;
        private readonly IMemoryCache memoryCache;

        public CachedGwpRepository(IGwpRepository gwpRepository, IMemoryCache memoryCache)
        {
            this.gwpRepository = gwpRepository;
            this.memoryCache = memoryCache;
        }

        public async Task<IDictionary<string, decimal>> GetGwps(string countryCode, IEnumerable<string> lobList)
        {
            var cacheKey = $"Gwps_{countryCode}_{string.Join("_", lobList)}";

            if (!memoryCache.TryGetValue(cacheKey, out IDictionary<string, decimal>? cachedResult) || cachedResult == null)
            {
                cachedResult = await gwpRepository.GetGwps(countryCode, lobList);
                var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
                memoryCache.Set(cacheKey, cachedResult, cacheOptions);
            }

            return cachedResult;
        }
    }
}
