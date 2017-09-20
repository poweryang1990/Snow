using System;
using System.Threading.Tasks;

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
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        void Set(CacheKey key, byte[] value, TimeSpan? expiry = null);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task SetAsync(CacheKey key, byte[] value, TimeSpan? expiry = null);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Get(CacheKey key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<byte[]> GetAsync(CacheKey key);

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="key"></param>
        void Refresh(CacheKey key);

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RefreshAsync(CacheKey key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(CacheKey key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(CacheKey key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="keyPrefix"></param>
        void RemoveByPrefix(CacheKey keyPrefix);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="keyPrefix"></param>
        /// <returns></returns>
        Task RemoveByPrefixAsync(CacheKey keyPrefix);

        /// <summary>
        /// 加锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool Lock(CacheKey key, string value, TimeSpan expiry);

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool ReleaseLock(CacheKey key, string value);

        /// <summary>
        /// 自增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long Increment(CacheKey key, long value = 1);

        /// <summary>
        /// 自减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long Decrement(CacheKey key, long value = 1);
    }
}