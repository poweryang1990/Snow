using System;
using UOKOFramework.Cache.Keys;

namespace UOKOFramework.Cache
{
    /// <summary>
    /// 锁
    /// </summary>
    public class LockProvider : ILockProvider
    {
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="distributedCache"></param>
        public LockProvider(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
        }

        /// <summary>
        /// 锁住资源，并在操作执行完或者过期时间后释放锁
        /// </summary>
        /// <param name="name">锁的名字</param>
        /// <param name="action">执行的操作</param>
        /// <param name="timeout">锁定时间，单位秒</param>
        /// <param name="throwIfLockFail">如果锁定失败则抛出异常</param>
        public void Lock(
            string name,
            Action action,
            int timeout = 60,
            bool throwIfLockFail = true)
        {
            Throws.ArgumentNullException(name, nameof(name));
            Throws.ArgumentNullException(action, nameof(action));

            var lockObject = new LockObject(name);

            if (this._distributedCache.Lock(lockObject, TimeSpan.FromSeconds(timeout)))
            {
                try
                {
                    action();
                }
                finally
                {
                    this._distributedCache.UnLock(lockObject);
                }
            }
            else
            {
                if (throwIfLockFail)
                {
                    throw new TipException("请求的资源处于繁忙中，请稍后重试。");
                }
            }
        }
    }
}