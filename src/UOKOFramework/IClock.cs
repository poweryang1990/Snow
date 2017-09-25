using System;

namespace UOKOFramework
{
    /// <summary>
    /// 时钟接口
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        DateTimeOffset Now { get; }

        /// <summary>
        /// 获取UnixTime
        /// </summary>
        int UnixTime { get; }
    }
}
