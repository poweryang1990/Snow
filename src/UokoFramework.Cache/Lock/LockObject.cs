using System;

namespace UOKOFramework.Cache.Lock
{
    /// <summary>
    /// 锁的对象
    /// </summary>
    public sealed class LockObject : CacheKey
    {
        /// <summary>
        /// Value
        /// </summary>
        public string Value => base.Name;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">锁的名字</param>
        public LockObject(string value) : base("lock")
        {
            base.Name = value;
            base.Build<LockObject>("id", Guid.NewGuid().ToString());
        }
    }
}