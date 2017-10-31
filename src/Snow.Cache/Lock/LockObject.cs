using System;

namespace Snow.Cache.Lock
{
    /// <summary>
    /// 锁的对象
    /// </summary>
    public sealed class LockObject
    {
        /// <summary>
        /// 锁的Key
        /// </summary>
        public CacheKey Key { get; }

        /// <summary>
        /// 锁的值
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">锁的名字</param>
        public LockObject(string value)
        {
            this.Value = value;
            this.Key = LockKey.Create(Guid.NewGuid()).Locked;
        }
    }
}