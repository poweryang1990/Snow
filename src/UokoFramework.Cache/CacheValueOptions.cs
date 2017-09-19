using System;

namespace UOKOFramework.Cache
{
    /// <summary>
    /// 缓存的值的配置
    /// </summary>
    public class CacheValueOptions
    {
        /// <summary>
        /// 绝对到期时间
        /// </summary>
        public DateTimeOffset? Absolute { get; set; }

        /// <summary>
        /// 相对到期时间
        /// </summary>
        public TimeSpan? Relative { get; set; }

        /// <summary>
        /// 缓存被删除前的 过期 时间
        /// 这个值将不会超出就绝对失效时间（如果设置了 绝对失效时间）， 
        /// </summary>
        public TimeSpan? Sliding { get; set; }
    }
}