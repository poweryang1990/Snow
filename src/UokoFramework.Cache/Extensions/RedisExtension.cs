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
            return value.ToJson();
        }

        public static TCache JsonToObject<TCache>(this RedisValue value)
        {
            return ((string)value).JsonToObject<TCache>();
        }
    }
}