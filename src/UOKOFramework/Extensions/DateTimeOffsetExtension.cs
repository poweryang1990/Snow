using System;

namespace UOKOFramework.Extensions
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
        /// 获取星座
        /// </summary>
        /// <param name="dateTime">this</param>
        /// <returns></returns>
        public static Constellation GetConstellation(this DateTimeOffset dateTime)
        {
            var month = dateTime.Month;
            var day = dateTime.Day;
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
