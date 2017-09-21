using System;

namespace UOKOFramework.Cache.Memory
{
    internal class MemoryObject
    {
        public MemoryObject(object value, DateTimeOffset? expiredTime)
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
        public DateTimeOffset? ExpireAt { private get; set; }

        public bool IsExpired(DateTimeOffset now)
        {
            if (this.ExpireAt == null)
            {
                return false;
            }
            return this.ExpireAt.Value < now;
        }
    }
}