using System;

namespace UOKOFramework
{
    /// <summary>
    /// 默认的时钟
    /// </summary>
    public class DefaultClock : IClock
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        public DateTimeOffset Now { get; set; } = DateTimeOffset.Now;
    }
}