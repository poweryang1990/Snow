using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snow.Cache
{
    public partial interface IDistributedCache
    {
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

    }
}