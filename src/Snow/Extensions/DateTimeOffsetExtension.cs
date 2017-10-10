using System;

namespace Snow.Extensions
{
    /// <summary>
    /// DateTimeOffset的扩展方法
    /// </summary>
    public static class DateTimeOffsetExtension
    {
        /// <summary>
        /// 获取UnixTime
        /// </summary>
        /// <param name="dateTimeOffset">this</param>
        /// <returns>UnixTime</returns>
        public static long ToUnixTimeSeconds(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.UtcTicks / 10000000L - 62135596800L;
        }

        /// <summary>
        /// 是否在两个时间点之间
        /// </summary>
        /// <param name="dateTime">this</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public static bool IsBetween(this DateTimeOffset dateTime, DateTimeOffset begin, DateTimeOffset end)
        {
            return begin < dateTime && dateTime < end;
        }

        /// <summary>
        /// 获取两个时间中较小的一个
        /// </summary>
        /// <param name="dateTime">this</param>
        /// <param name="that">另外一个时间</param>
        /// <returns></returns>
        public static DateTimeOffset Min(this DateTimeOffset dateTime, DateTimeOffset that)
        {
            return dateTime < that ? dateTime : that;
        }

        /// <summary>
        /// 获取两个时间中较大的一个
        /// </summary>
        /// <param name="dateTime">this</param>
        /// <param name="that">另外一个时间</param>
        /// <returns></returns>
        public static DateTimeOffset Max(this DateTimeOffset dateTime, DateTimeOffset that)
        {
            return dateTime > that ? dateTime : that;
        }


        /// <summary>
        /// 获取星座[枚举]
        /// </summary>
        /// <param name="dateTime">this</param>
        /// <returns></returns>
        public static Constellation GetConstellation(this DateTimeOffset dateTime)
        {
            var monthDay = Convert.ToInt32(dateTime.ToString("MMdd"));

            if (monthDay <= 119 || monthDay >= 1222)
            {
                return Constellation.Capricorn;
            }

            if (monthDay >= 120 && monthDay <= 218)
            {
                return Constellation.Aquarius;
            }

            if (monthDay >= 219 && monthDay <= 320)
            {
                return Constellation.Pisces;
            }

            if (monthDay >= 321 && monthDay <= 419)
            {
                return Constellation.Aries;
            }

            if (monthDay >= 420 && monthDay <= 520)
            {
                return Constellation.Taurus;
            }

            if (monthDay >= 521 && monthDay <= 621)
            {
                return Constellation.Gemini;
            }

            if (monthDay >= 622 && monthDay <= 722)
            {
                return Constellation.Cancer;
            }

            if (monthDay >= 723 && monthDay <= 822)
            {
                return Constellation.Leo;
            }

            if (monthDay >= 823 && monthDay <= 922)
            {
                return Constellation.Virgo;
            }

            if (monthDay >= 923 && monthDay <= 1023)
            {
                return Constellation.Libra;
            }

            if (monthDay >= 1024 && monthDay <= 1121)
            {
                return Constellation.Scorpio;
            }

            if (monthDay >= 1122 && monthDay <= 1221)
            {
                return Constellation.Sagittarius;
            }

            return Constellation.Unknown;
        }

        /// <summary>
        /// 获取星座[枚举的描述字符串]
        /// </summary>
        /// <param name="dateTime">this</param>
        /// <returns></returns>
        public static string GetConstellationString(this DateTimeOffset dateTime)
        {
            return GetConstellation(dateTime).GetDescription();
        }
        /// <summary>
        /// 获取本月天数
        /// </summary>
        /// <param name="dateTime">this</param>
        /// <returns>本月的天数</returns>
        public static int GetDaysInMonth(this DateTimeOffset dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }
    }
}
