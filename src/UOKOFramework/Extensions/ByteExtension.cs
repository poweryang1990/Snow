using System;
using System.Security.Cryptography;
using System.Text;
using UOKOFramework.Security;

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
            return new HashProvider(bytes).GetHash(hashAlgorithm);
        }

        /// <summary>
        /// 获取MD5
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>16byte的数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetMD5(this byte[] bytes)
        {
            return new HashProvider(bytes).GetMD5();
        }

        /// <summary>
        /// 获取SHA1
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>20byte的数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetSHA1(this byte[] bytes)
        {
            return new HashProvider(bytes).GetSHA1();
        }

        /// <summary>
        /// 获取SHA256
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>32byte的数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetSHA256(this byte[] bytes)
        {
            return new HashProvider(bytes).GetSHA256();

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
            return new HashProvider(bytes).GetHMACSHA1(key);

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
            return new ByteEncoder(bytes).GetHex(withHyphen, lowerCase);
        }

        /// <summary>
        /// bytes转base64
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>base64字符串</returns>
        public static string GetBase64(this byte[] bytes)
        {
            return new ByteEncoder(bytes).GetBase64();
        }

        /// <summary>
        /// bytes转url safe base64
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>url safe base64字符串</returns>
        public static string GetUrlSafeBase64(this byte[] bytes)
        {
            return new ByteEncoder(bytes).GetUrlSafeBase64();
        }

        /// <summary>
        /// bytes转字符串
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <param name="encoding">编码格式，默认采用UTF8编码</param>
        /// <returns></returns>
        public static string GetString(this byte[] bytes, Encoding encoding = null)
        {
            return new ByteEncoder(bytes).GetString(encoding);
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
            Throws.ArgumentNullException(bytes, nameof(bytes));
            Throws.ArgumentNullException(key, nameof(key));

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
        ///  AES解密[ECB,Zeros]
        /// </summary>
        /// <param name="encryptedBytes">被加密的byte数组</param>
        /// <param name="key">Key，长度只能是[16,24,32]</param>
        /// <returns>解密后的byte数组</returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] AESDecrypt(this byte[] encryptedBytes, byte[] key)
        {
            Throws.ArgumentNullException(encryptedBytes, nameof(encryptedBytes));
            Throws.ArgumentNullException(key, nameof(key));

            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new ArgumentException("无效的AES Key,必须是长度为16/24/32的byte数组");
            }
            using (var symmetricAlgorithm = new AesCryptoServiceProvider())
            {
                symmetricAlgorithm.Key = key;
                symmetricAlgorithm.Mode = CipherMode.ECB;
                symmetricAlgorithm.Padding = PaddingMode.Zeros;

                //解密
                using (var decryptor = symmetricAlgorithm.CreateDecryptor())
                {
                    var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                    //移除填充后的原始数据的有效长度
                    var originalDataLength = decryptedBytes.Length;
                    for (; originalDataLength > 0; originalDataLength--)
                    {
                        if (decryptedBytes[originalDataLength - 1] != 0)
                        {
                            break;
                        }
                    }

                    if (originalDataLength == decryptedBytes.Length)
                    {
                        return decryptedBytes;
                    }
                    else
                    {
                        var decryptedWithOutPaddingBytes = new byte[originalDataLength];
                        Buffer.BlockCopy(decryptedBytes, 0, decryptedWithOutPaddingBytes, 0, decryptedWithOutPaddingBytes.Length);
                        return decryptedWithOutPaddingBytes;
                    }
                }
            }
        }
    }
}