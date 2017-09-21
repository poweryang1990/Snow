using System;
using UOKOFramework.Cache.Extensions;
using UOKOFramework.Cache.Keys;

namespace UOKOFramework.Cache.Redis
{
    /// <summary>
    /// Redis实现的分布式缓存
    /// </summary>
    public partial class RedisDistributedCache
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="lockObject"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Lock(LockObject lockObject, TimeSpan expiry)
        {
            if (lockObject == null)
            {
                throw new ArgumentNullException(nameof(lockObject));
            }

            Connect();

            return this._db.LockTake(lockObject.ToRedisKey(), lockObject.Value, expiry);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lockObject"></param>
        /// <returns></returns>
        public bool UnLock(LockObject lockObject)
        {
            if (lockObject == null)
            {
                throw new ArgumentNullException(nameof(lockObject));
            }

            Connect();

            return this._db.LockRelease(lockObject.ToRedisKey(), lockObject.Value);
        }
    }
}