using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Snow.Cache.Lock;

namespace Snow.Cache
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

        #region 锁

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

        #endregion

        #region 自增 自减
        /// <summary>
        /// 自增
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <param name="value"></param>
        /// <returns>是否自增成功</returns>
        long Increment(CacheKey key, long value = 1);

        /// <summary>
        /// 自减
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <param name="value"></param>
        /// <returns>是否自减成功</returns>
        long Decrement(CacheKey key, long value = 1);

        #endregion

        #region Hash

        /// <summary>
        /// 向Hash中添加值
        /// </summary>
        /// <param name="key">Hash缓存的键</param>
        /// <param name="hashField">Hash字段</param>
        /// <param name="hashValue">Hash的值</param>
        /// <returns>添加是否成功</returns>
        bool HashSet<TCache>(CacheKey key, string hashField, TCache hashValue);

        /// <summary>
        /// 向Hash中添加值
        /// </summary>
        /// <param name="key">Hash缓存的键</param>
        /// <param name="hashItems">Hash条目</param>
        /// <returns>添加是否成功</returns>
        bool HashSet<TCache>(CacheKey key, IReadOnlyDictionary<string, TCache> hashItems);

        /// <summary>
        /// 向Hash中添加值
        /// </summary>
        /// <param name="key">Hash缓存的键</param>
        /// <param name="hashField">Hash字段</param>
        /// <param name="hashValue">Hash的值</param>
        /// <returns>添加是否成功</returns>
        Task<bool> HashSetAsync<TCache>(CacheKey key, string hashField, TCache hashValue);

        /// <summary>
        /// 向Hash中添加值
        /// </summary>
        /// <param name="key">Hash缓存的键</param>
        /// <param name="hashItems">Hash条目</param>
        /// <returns>添加是否成功</returns>
        Task<bool> HashSetAsync<TCache>(CacheKey key, IReadOnlyDictionary<string, TCache> hashItems);

        /// <summary>
        /// 从Hash中获取所有的字段
        /// </summary>
        /// <param name="key">Hash缓存的键</param>
        /// <returns>Hash的字段的集合</returns>
        IList<string> HashGetKeys(CacheKey key);

        /// <summary>
        /// 从Hash中获取所有的字段
        /// </summary>
        /// <param name="key">Hash缓存的键</param>
        /// <returns>Hash的字段的集合</returns>
        Task<IList<string>> HashGetKeysAsync(CacheKey key);

        /// <summary>
        /// 从Hash中获取指定的字段的值
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key">Hash缓存的键</param>
        /// <param name="hashField">Hash字段</param>
        /// <returns>Hash的值</returns>
        TCache HashGetValue<TCache>(CacheKey key, string hashField);

        /// <summary>
        /// 从Hash中获取指定的字段的值
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key">Hash缓存的键</param>
        /// <param name="hashField">Hash字段</param>
        /// <returns>Hash的值</returns>
        Task<TCache> HashGetValueAsync<TCache>(CacheKey key, string hashField);

        /// <summary>
        /// 从Hash中获取所有的字段的值
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key">Hash缓存的键</param>
        /// <returns>Hash的值得集合</returns>
        IList<TCache> HashGetValues<TCache>(CacheKey key);

        /// <summary>
        /// 从Hash中获取所有的字段的值
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key">Hash缓存的键</param>
        /// <returns>Hash的值得集合</returns>
        Task<IList<TCache>> HashGetValuesAsync<TCache>(CacheKey key);

        /// <summary>
        /// 从Hash中删除指定的字段
        /// </summary>
        /// <param name="key">Hash缓存的键</param>
        /// <param name="hashFields">Hash字段</param>
        /// <returns>删除是否成功</returns>
        bool HashDelete(CacheKey key, params string[] hashFields);

        /// <summary>
        /// 从Hash中删除指定的字段
        /// </summary>
        /// <param name="key">Hash缓存的键</param>
        /// <param name="hashFields">Hash字段</param>
        /// <returns>删除是否成功</returns>
        Task<bool> HashDeleteAsync(CacheKey key, params string[] hashFields);

        #endregion

    }
}