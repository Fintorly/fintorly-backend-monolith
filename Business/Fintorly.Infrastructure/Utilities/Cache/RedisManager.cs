using System;
using Fintorly.Application.Interfaces.Utils;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Fintorly.Infrastructure.Utilities.Cache
{
    public class RedisManager : IRedisService
    {
        IDistributedCache _distributedCache;
        static DistributedCacheEntryOptions options;
        public RedisManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            options = new DistributedCacheEntryOptions();
        }

        public async Task<T> GetStringAsync<T>(string key)
        {
            var result = await _distributedCache.GetStringAsync(key);
            if (result != null)
                return JsonConvert.DeserializeObject<T>(result)!;
            return default(T);
        }

        public async Task SetStringAsync<T>(string key, T value, DateTime expire)
        {
            var json = JsonConvert.SerializeObject(value);
            options.AbsoluteExpiration = expire;
            await _distributedCache.SetStringAsync(key, json, options);
        }

        public async Task SetStringAsync<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, json);
        }
    }
}

