using System;

namespace Snow.Cache.Lock
{
    /// <summary>
    /// 锁的键
    /// </summary>
    internal sealed class LockKey : CacheKey
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        private LockKey() : base("lock")
        {

        }

        /// <summary>
        /// 已锁定的
        /// </summary>
        public CacheKey Locked => base.Clone("locked");

        /// <summary>
        /// 创建一个新的LockKey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static LockKey Create(Guid id)
        {
            var lockKey = new LockKey();
            lockKey.SetParams("lock-id", id.ToString());
            return lockKey;
        }
    }
}
