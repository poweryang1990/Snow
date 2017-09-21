using System;

namespace UOKOFramework.Cache.Memory
{
    internal class MemoryObject
    {
        public MemoryObject(object value, DateTime? expiredTime)
        {
            Object = value;
            ExpireAt = expiredTime;
        }

        /// <summary>
        /// 原始对象
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireAt { private get; set; }

        public bool IsExpired(DateTime now)
        {
            if (this.ExpireAt == null)
            {
                return false;
            }
            return this.ExpireAt.Value < now;
        }
    }
}