using System;
using System.Collections.Generic;
using System.Linq;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// Guid的扩展方法
    /// </summary>
    public static class GuidExtension
    {
        /// <summary>
        /// 转换为String数组
        /// </summary>
        /// <param name="guids"></param>
        /// <param name="format">默认"N",["D", "N", "B", "P", "X"]</param>
        /// <returns></returns>
        public static string[] ToStringArray(this IEnumerable<Guid> guids, string format = "N")
        {
            if (guids == null)
            {
                return new string[0];
            }
            return guids.Select(id => id.ToString(format)).ToArray();
        }


        /// <summary>
        /// 转换为int[只截取Guid16个byte中的前4个byte]
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static int ToInt32(this Guid guid)
        {
            var guidBytes = guid.ToByteArray();
            var int32Bytes = new byte[4];
            Buffer.BlockCopy(guidBytes, 0, int32Bytes, 0, int32Bytes.Length);
            return BitConverter.ToInt32(int32Bytes, 0);
        }
    }
}