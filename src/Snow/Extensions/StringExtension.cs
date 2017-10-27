using System;
using System.Net;
using System.Text;
using Snow.Text;

namespace Snow.Extensions
{
    /// <summary>
    /// String的扩展方法
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 字符串转bytes
        /// </summary>
        /// <param name="value">原始字符串</param>
        /// <param name="encoding">编码格式，默认UTF8。</param>
        /// <returns></returns>
        public static byte[] GetBytes(this string value, Encoding encoding = null)
        {
            return ByteHelper.New().GetBytes(value, encoding);
        }

        /// <summary>
        /// base64字符串转bytes
        /// </summary>
        /// <param name="base64">base64字符串</param>
        /// <returns>原始bytes数组</returns>
        public static byte[] GetBytesFromBase64(this string base64)
        {
            return ByteHelper.New().GetBytesFromBase64(base64);
        }

        /// <summary>
        /// hex字符串转bytes
        /// </summary>
        /// <param name="hex">hex字符串</param>
        /// <returns>原始bytes数组</returns>
        public static byte[] GetBytesFromHex(this string hex)
        {
            return ByteHelper.New().GetBytesFromHex(hex);
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

            return value
                .GetBytes(encoding)
                .GetMD5()
                .GetHex(false, lowerCase);
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

            return value
                .GetBytes(encoding)
                .GetSHA1()
                .GetHex(false, lowerCase);
        }


        /// <summary>
        /// 字符串转枚举[失败返回默认值]
        /// </summary>
        /// <param name="value">枚举的name字符串</param>
        /// <param name="defaultValue">枚举的默认值</param>
        /// <returns>枚举对象</returns>
        public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            if (Enum.TryParse(value, true, out TEnum enumValue) == true)
            {
                return enumValue;
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转枚举[失败返回Null]
        /// </summary>
        /// <param name="value">枚举的name字符串</param>
        /// <returns>枚举对象</returns>
        public static TEnum? ToEnumOrNull<TEnum>(this string value) where TEnum : struct
        {
            if (Enum.TryParse(value, true, out TEnum enumValue) == true)
            {
                return enumValue;
            }
            return null;
        }

        /// <summary>
        /// 字符串转Guid[失败时返回Guid.Empty]
        /// </summary>
        /// <param name="value">Guid字符串</param>
        /// <returns>Guid</returns>
        public static Guid ToGuid(this string value)
        {
            if (Guid.TryParse(value, out var guid) == true)
            {
                return guid;
            }
            return Guid.Empty;
        }

        /// <summary>
        /// 如果当前string是null或者空，则返回指定的defaultValue
        /// </summary>
        /// <param name="value">this</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string Default(this string value, string defaultValue)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            return value;
        }

        /// <summary>
        /// 检查是否包含指定的字符串
        /// </summary>
        /// <param name="this">原始字符串</param>
        /// <param name="value">指定的字符串</param>
        /// <param name="comparisonType">比较方式[默认忽略大小写]</param>
        /// <returns></returns>
        public static bool Include(
            this string @this,
            string value,
            StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(@this) || string.IsNullOrEmpty(value))
            {
                return false;
            }
            return @this.IndexOf(value, comparisonType) != -1;
        }

        /// <summary>
        /// 比较两个字符串是否相等
        /// </summary>
        /// <param name="this">this</param>
        /// <param name="other">指定的字符串</param>
        /// <param name="comparisonType">比较方式[默认忽略大小写]</param>
        /// <returns></returns>
        public static bool IsEqual(
            this string @this,
            string other,
            StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {

            return string.Equals(@this, other, comparisonType);
        }
    }
}
