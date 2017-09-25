using StackExchange.Redis;

namespace UOKOFramework.Cache.Extensions
{
    internal static class CacheKeyExtensions
    {
        public static RedisKey ToRedisKey(this CacheKey cacheKey)
        {
            return cacheKey.ToString();
        }

        public static RedisKey[] ToRedisKeys(this CacheKey cacheKey)
        {
            return new RedisKey[]
            {
                cacheKey.ToString()
            };
        }
    }
}