using System;
using System.Threading.Tasks;
using UOKOFramework.Cache.Keys;

namespace UOKOFramework.Cache
{
    /// <summary>
    /// 分布式缓存
    /// </summary>
    public interface IDistributedCache
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <param name="value">缓存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>是否成功</returns>
        bool Set<TCache>(CacheKey key, TCache value, TimeSpan? expiry = null);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <param name="value">缓存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>是否成功</returns>
        Task<bool> SetAsync<TCache>(CacheKey key, TCache value, TimeSpan? expiry = null);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <returns>缓存的值</returns>
        TCache Get<TCache>(CacheKey key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <returns>缓存的值</returns>
        Task<TCache> GetAsync<TCache>(CacheKey key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        bool Remove(CacheKey key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(CacheKey key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="keyPrefix"></param>
        bool RemoveByPrefix(CacheKey keyPrefix);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="keyPrefix"></param>
        /// <returns></returns>
        Task<bool> RemoveByPrefixAsync(CacheKey keyPrefix);

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

        /// <summary>
        /// 自增
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <param name="value"></param>
        /// <returns>是否成功</returns>
        long Increment(CacheKey key, long value = 1);

        /// <summary>
        /// 自减
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <param name="value"></param>
        /// <returns>是否成功</returns>
        long Decrement(CacheKey key, long value = 1);
    }
}