using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RedisTest.Extention
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, 
            string recordId, 
            T data, 
            TimeSpan? absoluteExpireTime = null, 
            TimeSpan? unusedExpireTime = null)
        {
            var option = new DistributedCacheEntryOptions();
            option.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            option.SlidingExpiration = unusedExpireTime ?? TimeSpan.FromSeconds(60);

            var jData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jData, option);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jData = await cache.GetAsync(recordId);

            if (jData == null)
                return default(T);

            return JsonSerializer.Deserialize<T>(jData);
        }
    }
}
