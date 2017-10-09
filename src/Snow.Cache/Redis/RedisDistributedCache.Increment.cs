using System;
using Snow.Cache.Extensions;

namespace Snow.Cache.Redis
{
    /// <summary>
    /// Redis实现的分布式缓存
    /// </summary>
    public partial class RedisDistributedCache
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Increment(CacheKey key, long value = 1)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Connect();

            return this._db.StringIncrement(key.ToRedisKey(), value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Decrement(CacheKey key, long value = 1)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Connect();

            return this._db.StringDecrement(key.ToRedisKey(), value);
        }
    }
}