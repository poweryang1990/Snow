using System;
using System.Security.Cryptography;
using System.Text;
using UOKOFramework.Security;
using UOKOFramework.Text;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// Byte的扩展方法
    /// </summary>
    public static class ByteExtension
    {
        /// <summary>
        /// 获取散列值
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <param name="hashAlgorithm">具体的hash算法</param>
        /// <returns>hash字节数组</returns>
        public static byte[] GetHash(this byte[] bytes, HashAlgorithm hashAlgorithm)
        {
            return new HashHelper(bytes).GetHash(hashAlgorithm);
        }

        /// <summary>
        /// 获取MD5
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>16byte的数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetMD5(this byte[] bytes)
        {
            return new HashHelper(bytes).GetMD5();
        }

        /// <summary>
        /// 获取SHA1
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>20byte的数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetSHA1(this byte[] bytes)
        {
            return new HashHelper(bytes).GetSHA1();
        }

        /// <summary>
        /// 获取SHA256
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>32byte的数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetSHA256(this byte[] bytes)
        {
            return new HashHelper(bytes).GetSHA256();

        }

        /// <summary>
        /// 获取HMACSHA1
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <param name="key">MAC的密钥</param>
        /// <returns>20byte的数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetHMACSHA1(this byte[] bytes, byte[] key)
        {
            return new HashHelper(bytes).GetHMACSHA1(key);

        }


        /// <summary>
        /// bytes转16进制
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <param name="withHyphen">使用分隔符"-"分割，默认true。</param>
        /// <param name="lowerCase">小写，默认false。</param>
        /// <returns>16进制的字符串</returns>
        public static string GetHex(this byte[] bytes, bool withHyphen = true, bool lowerCase = false)
        {
            return new ByteHelper().GetHex(bytes, withHyphen, lowerCase);
        }

        /// <summary>
        /// bytes转base64
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>base64字符串</returns>
        public static string GetBase64(this byte[] bytes)
        {
            return new ByteHelper().GetBase64(bytes);
        }

        /// <summary>
        /// bytes转url safe base64
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>url safe base64字符串</returns>
        public static string GetUrlSafeBase64(this byte[] bytes)
        {
            return new ByteHelper().GetUrlSafeBase64(bytes);
        }

        /// <summary>
        /// bytes转字符串
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <param name="encoding">编码格式，默认采用UTF8编码</param>
        /// <returns></returns>
        public static string GetString(this byte[] bytes, Encoding encoding = null)
        {
            return new ByteHelper().GetString(bytes, encoding);
        }

        /// <summary>
        ///  AES加密[ECB,Zeros]
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <param name="key">Key，长度只能是[16,24,32]</param>
        /// <returns>加密后的byte数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] AESEncrypt(this byte[] bytes, byte[] key)
        {
            return new AESHelper(key).Encrypt(bytes);
        }

        /// <summary>
        ///  AES解密[ECB,Zeros]
        /// </summary>
        /// <param name="encryptedBytes">被加密的byte数组</param>
        /// <param name="key">Key，长度只能是[16,24,32]</param>
        /// <returns>解密后的byte数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] AESDecrypt(this byte[] encryptedBytes, byte[] key)
        {
            return new AESHelper(key).Decrypt(encryptedBytes);
        }

        /// <summary>
        /// 合并数组
        /// </summary>
        /// <param name="current">当前数组</param>
        /// <param name="next">下一个数组</param>
        /// <returns>合并后的数组</returns>
        public static T[] Combine<T>(this T[] current, T[] next)
        {
            Throws.ArgumentNullException(current, nameof(current));
            Throws.ArgumentNullException(next, nameof(next));

            var result = new T[current.Length + next.Length];

            Buffer.BlockCopy(current, 0, result, 0, current.Length);
            Buffer.BlockCopy(next, 0, result, current.Length, next.Length);

            return result;
        }
    }
}