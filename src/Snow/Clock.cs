using System;
using Snow.Extensions;

namespace Snow
{
    /// <summary>
    /// 默认的时钟
    /// </summary>
    public class Clock : IClock
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        public DateTimeOffset Now { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// 获取UnixTime
        /// </summary>
        public int UnixTime => (int)Now.ToUnixTimeSeconds();
    }
}