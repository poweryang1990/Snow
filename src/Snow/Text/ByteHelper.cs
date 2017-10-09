using System;
using System.Text;

// ReSharper disable InconsistentNaming

namespace Snow.Text
{
    /// <summary>
    /// Byte帮助类
    /// </summary>
    public class ByteHelper
    {
        /// <summary>
        /// 默认的编码格式
        /// </summary>
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// 获取16进制
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <param name="withHyphen">使用分隔符"-"分割，默认true。</param>
        /// <param name="lowerCase">小写，默认false。</param>
        /// <returns>16进制的字符串</returns>
        public string GetHex(byte[] bytes, bool withHyphen = true, bool lowerCase = false)
        {
            Throws.ArgumentNullException(bytes, nameof(bytes));

            var hexString = BitConverter.ToString(bytes);

            if (withHyphen == false)
            {
                hexString = hexString.Replace("-", "");
            }

            if (lowerCase == true)
            {
                hexString = hexString.ToLower();
            }

            return hexString;
        }

        /// <summary>
        /// 获取base64
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>base64字符串</returns>
        public string GetBase64(byte[] bytes)
        {
            Throws.ArgumentNullException(bytes, nameof(bytes));
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 获取转url safe的base64
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>url safe base64字符串</returns>
        public string GetUrlSafeBase64(byte[] bytes)
        {
            return GetBase64(bytes)
                .Replace('+', '-')
                .Replace('/', '_');
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <param name="encoding">编码格式，默认采用UTF8编码</param>
        /// <returns></returns>
        public string GetString(byte[] bytes, Encoding encoding = null)
        {
            Throws.ArgumentNullException(bytes, nameof(bytes));
            if (encoding == null)
            {
                encoding = DefaultEncoding;
            }
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 字符串转bytes
        /// </summary>
        /// <param name="value">原始字符串</param>
        /// <param name="encoding">编码格式，默认UTF8。</param>
        /// <returns></returns>
        public byte[] GetBytes(string value, Encoding encoding = null)
        {
            Throws.ArgumentNullException(value, nameof(value));

            if (encoding == null)
            {
                encoding = DefaultEncoding;
            }
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// base64字符串转bytes
        /// </summary>
        /// <param name="base64">base64字符串</param>
        /// <returns>原始bytes数组</returns>
        public byte[] GetBytesFromBase64(string base64)
        {
            Throws.ArgumentNullException(base64, nameof(base64));
            if (base64.IndexOf('-') != -1)
            {
                base64 = base64.Replace('-', '+').Replace('_', '/');
            }
            return Convert.FromBase64String(base64);
        }
    }
}
