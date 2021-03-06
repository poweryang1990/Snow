﻿using System;
using System.Threading.Tasks;

namespace Snow.Cache
{
    /// <summary>
    /// 分布式缓存
    /// </summary>
    public partial interface IDistributedCache
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
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <param name="keyAsPrefix">把缓存的键作为前缀来执行批量删除</param>
        /// <returns>是否删除成功</returns>
        bool Delete(CacheKey key, bool keyAsPrefix = false);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <param name="keyAsPrefix">把缓存的键作为前缀来执行批量删除</param>
        /// <returns>是否删除成功</returns>
        Task<bool> DeleteAsync(CacheKey key, bool keyAsPrefix = false);
    }
}