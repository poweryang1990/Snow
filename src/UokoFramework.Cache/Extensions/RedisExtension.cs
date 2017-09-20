using StackExchange.Redis;
using UOKOFramework.Serialization.Extensions;

namespace UOKOFramework.Cache.Extensions
{
    internal static class RedisExtension
    {
        public static RedisKey ToRedisKey(this CacheKey cacheKey)
        {
            return cacheKey.ToString();
        }

        public static RedisValue ToRedisValue(this object value)
        {
            return value.ToJSON();
        }

        public static TCache ToObject<TCache>(this RedisValue value)
        {
            return ((string)value).ToObject<TCache>();
        }
    }
}