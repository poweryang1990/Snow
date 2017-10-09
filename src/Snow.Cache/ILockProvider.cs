using System;

namespace Snow.Cache
{
    /// <summary>
    /// 锁的接口
    /// </summary>
    public interface ILockProvider
    {
        /// <summary>
        /// 锁住资源，并在操作执行完或者过期时间后释放锁
        /// </summary>
        /// <param name="name">锁的名字</param>
        /// <param name="action">执行的操作</param>
        /// <param name="timeout">锁定时间，单位秒</param>
        /// <param name="throwIfLockFail">如果锁定失败则抛出异常</param>
        void Lock(string name, Action action, int timeout = 60, bool throwIfLockFail = true);
    }
}