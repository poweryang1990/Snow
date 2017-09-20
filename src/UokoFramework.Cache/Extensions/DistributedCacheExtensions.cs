using System;
using System.Text;
using System.Threading.Tasks;
using UOKOFramework.Extensions;

// ReSharper disable once CheckNamespace
namespace UOKOFramework.Cache
{
    /// <summary>
    /// IDistributedCache的扩展方法
    /// </summary>
    public static class DistributedCacheExtensions
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Encoding UTF8 = Encoding.UTF8;

        /// <summary>
        /// 设置字符串
        /// </summary>
        /// <param name="distributedCacheProvider"></param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiry">过期时间</param>
        public static void SetString(
            this IDistributedCache distributedCacheProvider,
            CacheKey key,
            string value,
            TimeSpan? expiry = null)
        {
            Throws.ArgumentNullException(value, nameof(value));

            distributedCacheProvider.Set(key, value.GetBytes(UTF8), expiry);
        }

        /// <summary>
        /// 设置字符串
        /// </summary>
        /// <param name="distributedCacheProvider"></param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public static Task SetStringAsync(
            this IDistributedCache distributedCacheProvider,
            CacheKey key,
            string value,
            TimeSpan? expiry = null)
        {
            Throws.ArgumentNullException(value, nameof(value));

            return distributedCacheProvider.SetAsync(key, value.GetBytes(UTF8), expiry);
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="distributedCacheProvider"></param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetString(
            this IDistributedCache distributedCacheProvider,
            CacheKey key)
        {
            return distributedCacheProvider.Get(key).GetString(UTF8);

        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="distributedCacheProvider"></param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static async Task<string> GetStringAsync(
            this IDistributedCache distributedCacheProvider,
            CacheKey key)
        {
            var bytes= await distributedCacheProvider.GetAsync(key);
            return bytes.GetString(UTF8);
        }
        
    }
}