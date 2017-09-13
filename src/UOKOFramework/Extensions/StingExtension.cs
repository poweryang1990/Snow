using System;
using System.Text;

namespace UokoFramework.Extensions
{
    /// <summary>
    /// String的扩展方法
    /// </summary>
    public static class StingExtension
    {
        /// <summary>
        /// 字符串转bytes
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="encoding">编码格式，默认UTF8</param>
        /// <returns></returns>
        public static byte[] GetBytes(this string value, Encoding encoding = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return encoding.GetBytes(value);
        }
    }
}
