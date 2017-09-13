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
        /// <param name="value">原始字符串</param>
        /// <param name="encoding">编码格式，默认UTF8。</param>
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

        /// <summary>
        /// 获取MD5
        /// </summary>
        /// <param name="value">原始字符串</param>
        /// <param name="encoding">字符串编码，默认UTF8。</param>
        /// <param name="lowerCase">小写，默认false。</param>
        /// <returns>MD5的16进制字符串，32位</returns>
        // ReSharper disable once InconsistentNaming
        public static string GetMD5(this string value, Encoding encoding = null, bool lowerCase = false)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return value
                .GetBytes(encoding)
                .GetMD5()
                .GetHex(false, lowerCase);
        }

        /// <summary>
        /// 校验MD5
        /// </summary>
        /// <param name="value">原始的字符串</param>
        /// <param name="expectMd5">期望的md5值</param>
        /// <param name="encoding">字符串编码，默认UTF8。</param>
        /// <returns>MD5是否匹配</returns>
        // ReSharper disable once InconsistentNaming
        public static bool VerifyMD5(this string value, string expectMd5, Encoding encoding = null)
        {
            if (expectMd5 == null)
            {
                throw new ArgumentNullException(nameof(expectMd5));
            }
            if (expectMd5.Length != 32)
            {
                throw new ArgumentException($"[{expectMd5}]不是有效的MD5。");
            }
            var actualMd5 = value.GetMD5(encoding);
            return string.Equals(actualMd5, expectMd5, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 获取MD5
        /// </summary>
        /// <param name="value">原始字符串</param>
        /// <param name="encoding">字符串编码，默认UTF8。</param>
        /// <param name="lowerCase">小写，默认false。</param>
        /// <returns>SHA1的16进制字符串，40位</returns>
        // ReSharper disable once InconsistentNaming
        public static string GetSHA1(this string value, Encoding encoding = null, bool lowerCase = false)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return value
                .GetBytes(encoding)
                .GetSHA1()
                .GetHex(false, lowerCase);
        }

        /// <summary>
        /// 校验SHA1
        /// </summary>
        /// <param name="value">原始的字符串</param>
        /// <param name="expectSha1">期望的SHA1值</param>
        /// <param name="encoding">字符串编码，默认UTF8。</param>
        /// <returns>SHA1是否匹配</returns>
        // ReSharper disable once InconsistentNaming
        public static bool VerifySHA1(this string value, string expectSha1, Encoding encoding = null)
        {
            if (expectSha1 == null)
            {
                throw new ArgumentNullException(nameof(expectSha1));
            }
            if (expectSha1.Length != 40)
            {
                throw new ArgumentException($"[{expectSha1}]不是有效的SHA1。");
            }
            var actualSha1 = value.GetSHA1(encoding);
            return string.Equals(actualSha1, expectSha1, StringComparison.OrdinalIgnoreCase);
        }
    }
}
