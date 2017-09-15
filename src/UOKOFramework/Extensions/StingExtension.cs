using System;
using System.Text;

namespace UOKOFramework.Extensions
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
            Throws.ArgumentNullException(value, nameof(value));

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// base64字符串转bytes
        /// </summary>
        /// <param name="base64">base64字符串</param>
        /// <returns>原始bytes数组</returns>
        public static byte[] GetBytesFromBase64(this string base64)
        {
            Throws.ArgumentNullException(base64, nameof(base64));
            if (base64.IndexOf('-') != -1)
            {
                base64 = base64.Replace('-', '+').Replace('_', '/');
            }
            return Convert.FromBase64String(base64);
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
            Throws.ArgumentNullException(value, nameof(value));

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
            Throws.ArgumentNullException(expectMd5, nameof(expectMd5));

            if (expectMd5.Length != 32)
            {
                throw new ArgumentException($"[{expectMd5}]不是有效的MD5。");
            }
            var actualMd5 = value.GetMD5(encoding);
            return string.Equals(actualMd5, expectMd5, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 获取SHA1
        /// </summary>
        /// <param name="value">原始字符串</param>
        /// <param name="encoding">字符串编码，默认UTF8。</param>
        /// <param name="lowerCase">小写，默认false。</param>
        /// <returns>SHA1的16进制字符串，40位</returns>
        // ReSharper disable once InconsistentNaming
        public static string GetSHA1(this string value, Encoding encoding = null, bool lowerCase = false)
        {
            Throws.ArgumentNullException(value, nameof(value));
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
            Throws.ArgumentNullException(expectSha1, nameof(expectSha1));
            if (expectSha1.Length != 40)
            {
                throw new ArgumentException($"[{expectSha1}]不是有效的SHA1。");
            }
            var actualSha1 = value.GetSHA1(encoding);
            return string.Equals(actualSha1, expectSha1, StringComparison.OrdinalIgnoreCase);
        }
    }
}
