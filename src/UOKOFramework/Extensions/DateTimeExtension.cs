using System;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// DateTime的扩展方法
    /// </summary>
    public static class DateTimeExtension
    {
        private static readonly DateTime UnixTimeStartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 获取UnixTime
        /// </summary>
        /// <param name="this">this</param>
        /// <returns>UnixTime</returns>
        public static int GetUnixtime(this DateTime @this)
        {
            return (int)(@this - UnixTimeStartTime).TotalSeconds;
        }

        /// <summary>
        /// 是否在两个时间点之间
        /// </summary>
        /// <param name="this">this</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public static bool IsBetween(this DateTime @this, DateTime begin, DateTime end)
        {
            return begin < @this && @this < end;
        }

        /// <summary>
        /// 获取两个时间中较小的一个
        /// </summary>
        /// <param name="this">this</param>
        /// <param name="that">另外一个时间</param>
        /// <returns></returns>
        public static DateTime Min(this DateTime @this, DateTime that)
        {
            return @this < that ? @this : that;
        }

        /// <summary>
        /// 获取两个时间中较大的一个
        /// </summary>
        /// <param name="this">this</param>
        /// <param name="that">另外一个时间</param>
        /// <returns></returns>
        public static DateTime Max(this DateTime @this, DateTime that)
        {
            return @this > that ? @this : that;
        }

        /// <summary>
        /// 获取星座
        /// </summary>
        /// <param name="this">this</param>
        /// <returns></returns>
        public static Constellation GetConstellation(this DateTime @this)
        {
            var month = @this.Month;
            var day = @this.Day;
            switch (month)
            {
                case 1:
                    if (day <= 19)
                        return Constellation.Capricorn;
                    else
                        return Constellation.Aquarius;
                case 2:
                    if (day <= 18)
                        return Constellation.Aquarius;
                    else
                        return Constellation.Pisces;
                case 3:
                    if (day <= 20)
                        return Constellation.Pisces;
                    else
                        return Constellation.Aries;
                case 4:
                    if (day <= 19)
                        return Constellation.Aries;
                    else
                        return Constellation.Taurus;
                case 5:
                    if (day <= 20)
                        return Constellation.Taurus;
                    else
                        return Constellation.Gemini;
                case 6:
                    if (day <= 20)
                        return Constellation.Gemini;
                    else
                        return Constellation.Cancer;
                case 7:
                    if (day <= 22)
                        return Constellation.Cancer;
                    else
                        return Constellation.Leo;
                case 8:
                    if (day <= 22)
                        return Constellation.Leo;
                    else
                        return Constellation.Virgo;
                case 9:
                    if (day <= 22)
                        return Constellation.Virgo;
                    else
                        return Constellation.Libra;
                case 10:
                    if (day <= 22)
                        return Constellation.Libra;
                    else
                        return Constellation.Scorpio;
                case 11:
                    if (day <= 21)
                        return Constellation.Scorpio;
                    else
                        return Constellation.Sagittarius;
                case 12:
                    if (day <= 21)
                        return Constellation.Sagittarius;
                    else
                        return Constellation.Capricorn;
            }
            return Constellation.Unknown;
        }
    }
}
