using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static async Task SaveRecordAsync<T>(this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime,
                SlidingExpiration = unusedExpireTime
            };

            await cache.SetStringAsync(recordId, JsonSerializer.Serialize(data), options);
        }

        public static async ValueTask<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData is null)
                return default;

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public static async Task RemoveRecordAsync(this IDistributedCache cache, string recordId)
        {
            await cache.RemoveAsync(recordId);
        }

        public static void RemoveRecords(this IDistributedCache cache, IEnumerable<string> recordIds)
        {
            Parallel.ForEach(recordIds, async id => await cache.RemoveAsync(id));
        }
    }
}
