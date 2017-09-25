using System;
using System.Text;
// ReSharper disable InconsistentNaming

namespace UOKOFramework.Security
{
    /// <summary>
    /// ASCII帮助类
    /// </summary>
    public class ASCIIHelper
    {
        /// <summary>
        /// 获取原始byte数组
        /// </summary>
        public byte[] Bytes { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        public ASCIIHelper(byte[] bytes)
        {
            Throws.ArgumentNullException(bytes, nameof(bytes));
            this.Bytes = bytes;
        }

        /// <summary>
        /// 获取16进制
        /// </summary>
        /// <param name="withHyphen">使用分隔符"-"分割，默认true。</param>
        /// <param name="lowerCase">小写，默认false。</param>
        /// <returns>16进制的字符串</returns>
        public string GetHex(bool withHyphen = true, bool lowerCase = false)
        {
            var hexString = BitConverter.ToString(this.Bytes);

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
        /// <returns>base64字符串</returns>
        public string GetBase64()
        {
            return Convert.ToBase64String(this.Bytes);
        }

        /// <summary>
        /// 获取转url safe的base64
        /// </summary>
        /// <returns>url safe base64字符串</returns>
        public string GetUrlSafeBase64()
        {
            return GetBase64()
                .Replace('+', '-')
                .Replace('/', '_');
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="encoding">编码格式，默认采用UTF8编码</param>
        /// <returns></returns>
        public string GetString(Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return encoding.GetString(this.Bytes);
        }
    }
}
