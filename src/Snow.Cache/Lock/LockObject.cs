using System;

namespace Snow.Cache.Lock
{
    /// <summary>
    /// 锁的对象
    /// </summary>
    public sealed class LockObject : CacheKey
    {
        /// <summary>
        /// 锁的值
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">锁的名字</param>
        public LockObject(string name) : base("lock", name)
        {
            this.Token = name;
            base.SetParamsCore("id", Guid.NewGuid().ToString());
        }
    }
}