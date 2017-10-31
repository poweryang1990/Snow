using System;
using Snow.Cache.Lock;

namespace Snow.Cache
{
    public partial interface IDistributedCache
    {
        /// <summary>
        /// 加锁
        /// </summary>
        /// <param name="lockObject">锁</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>是否锁定成功</returns>
        bool Lock(LockObject lockObject, TimeSpan expiry);

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="lockObject">锁</param>
        /// <returns>是否释放成功</returns>
        bool UnLock(LockObject lockObject);
    }
}