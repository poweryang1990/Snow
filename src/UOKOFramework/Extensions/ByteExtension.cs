using System;
using System.Security.Cryptography;
using System.Text;

namespace UokoFramework.Extensions
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
        private static byte[] GetHash(this byte[] bytes, HashAlgorithm hashAlgorithm)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            if (hashAlgorithm == null)
            {
                throw new ArgumentNullException(nameof(hashAlgorithm));
            }
            using (hashAlgorithm)
            {
                return hashAlgorithm.ComputeHash(bytes);
            }
        }

        /// <summary>
        /// 获取MD5
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>16byte的数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetMD5(this byte[] bytes)
        {
            return bytes.GetHash(new MD5CryptoServiceProvider());
        }

        /// <summary>
        /// 获取SHA1
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>20byte的数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetSHA1(this byte[] bytes)
        {
            return bytes.GetHash(new SHA1CryptoServiceProvider());
        }

        /// <summary>
        /// 获取SHA256
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>32byte的数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetSHA256(this byte[] bytes)
        {
            return bytes.GetHash(new SHA256CryptoServiceProvider());
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
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
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
        /// bytes转base64
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>base64字符串</returns>
        public static string GetBase64(this byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// bytes转字符串
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <param name="encoding">编码格式，默认采用UTF8编码</param>
        /// <returns></returns>
        public static string GetString(this byte[] bytes, Encoding encoding = null)
        {

            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return encoding.GetString(bytes);
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
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            if (key == null || key.Length == 0)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new ArgumentException("无效的AES Key,必须是长度为16/24/32的byte数组");
            }
            using (var symmetricAlgorithm = new AesCryptoServiceProvider())
            {
                symmetricAlgorithm.Key = key;
                symmetricAlgorithm.Mode = CipherMode.ECB;
                symmetricAlgorithm.Padding = PaddingMode.Zeros;
                //加密
                using (var encryptor = symmetricAlgorithm.CreateEncryptor())
                {
                    return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
                }
            }
        }

        /// <summary>
        ///  AES解密[ECB,None]
        /// </summary>
        /// <param name="bytes">加密的byte数组</param>
        /// <param name="key">Key，长度只能是[16,24,32]</param>
        /// <returns>解密后的byte数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] AESDecrypt(this byte[] bytes, byte[] key)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            if (key == null || key.Length == 0)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new ArgumentException("无效的AES Key,必须是长度为16/24/32的byte数组");
            }
            using (var symmetricAlgorithm = new AesCryptoServiceProvider())
            {
                symmetricAlgorithm.Key = key;
                symmetricAlgorithm.Mode = CipherMode.ECB;
                symmetricAlgorithm.Padding = PaddingMode.None;

                //解密
                using (var decryptor = symmetricAlgorithm.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
                }
            }
        }
    }
}