﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UokoFramework.OCR.Tencent.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        public static long ToUnixTime(this DateTime nowTime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }
    }
}
