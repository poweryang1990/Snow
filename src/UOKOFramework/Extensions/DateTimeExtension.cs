using System;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// DateTime的扩展方法。
    /// </summary>
    [Obsolete("请使用DateTimeOffset类型代替DateTime")]
    public static class DateTimeExtension
    {
        private static readonly DateTime UnixTimeStartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 获取UnixTime
        /// </summary>
        /// <param name="dateTime">this</param>
        /// <returns>UnixTime</returns>
        [Obsolete("请使用DateTimeOffset类型代替DateTime")]
        public static int ToUnixTimeSeconds(this DateTime dateTime)
        {
            return (int)(dateTime - UnixTimeStartTime).TotalSeconds;
        }
    }
}
