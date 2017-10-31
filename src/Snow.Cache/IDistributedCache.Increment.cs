namespace Snow.Cache
{
    public partial interface IDistributedCache
    {
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
    }
}