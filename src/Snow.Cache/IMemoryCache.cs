using System;
using System.Collections.Generic;

namespace Snow.Cache
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public interface IMemoryCache
    {
        /// <summary>
        /// 添加缓存项，如果Key存在就替换
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiry">相对的过期时间</param>
        /// <returns>添加是否成功</returns>
        bool Set(CacheKey key, object value, TimeSpan? expiry = null);

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>如果不存在，返回为Null</returns>
        object Get(CacheKey key);

        /// <summary>
        /// 返回强类型的缓存项
        /// </summary>
        /// <typeparam name="T">Value的类型</typeparam>
        /// <param name="key">Key</param>
        /// <returns>如果不存在，返回Default(T）</returns>
        T Get<T>(CacheKey key);

        /// <summary>
        /// 获取缓存项集合
        /// </summary>
        /// <param name="keys">Key集合</param>
        /// <returns>返回Key-Value集合</returns>
        IDictionary<CacheKey, object> Get(IEnumerable<CacheKey> keys);

        /// <summary>
        /// 删除指定Key的缓存
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="keyAsPrefix"></param>
        /// <returns>如果Key存在且删除成功，返回true；否则为false</returns>
        bool Delete(CacheKey key, bool keyAsPrefix = false);

    }
}
