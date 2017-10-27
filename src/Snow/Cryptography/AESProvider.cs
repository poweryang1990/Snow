using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
// ReSharper disable InconsistentNaming

namespace Snow.Cryptography
{
    /// <summary>
    /// AES帮助类
    /// </summary>
    public class AESProvider
    {
        /// <summary>
        /// 创建新的AESHelper对象
        /// </summary>
        /// <param name="key">key：长度只能是[16,24,32]</param>
        /// <returns></returns>
        public static AESProvider New(byte[] key)
        {
            return new AESProvider
            {
                Key = key
            };
        }

        private byte[] _key;

        /// <summary>
        /// 获取或设置Key
        /// <para>key：长度只能是[16,24,32]</para>
        /// </summary>
        public byte[] Key
        {
            get => this._key;
            set
            {
                Throws.ArgumentNullException(value, nameof(Key));

                if (value.Length != 16 && value.Length != 24 && value.Length != 32)
                {
                    throw new ArgumentException("无效的AES Key,必须是长度为16/24/32的byte数组");
                }
                this._key = value;
            }
        }

        /// <summary>
        /// 获取或设置工作模式，默认CBC
        /// </summary>
        public CipherMode Mode { get; set; } = CipherMode.CBC;

        /// <summary>
        /// 获取或设置填充模式，默认PKCS7
        /// </summary>
        public PaddingMode Padding { get; set; } = PaddingMode.PKCS7;

        /// <summary>
        /// 使用IV，默认True
        /// </summary>
        public bool WithIV { get; set; } = true;

        /// <summary>
        ///  加密
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>加密后的byte数组</returns>
        public byte[] Encrypt(byte[] bytes)
        {
            Throws.ArgumentNullException(bytes, nameof(bytes));

            using (var aes = BuildAesAlgorithm())
            {
                if (this.WithIV == true)
                {
                    aes.GenerateIV();
                    using (var encryptor = aes.CreateEncryptor())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            memoryStream.Write(aes.IV, 0, 16);
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(bytes, 0, bytes.Length);
                                cryptoStream.FlushFinalBlock();
                            }
                            return memoryStream.ToArray();
                        }
                    }
                }
                else
                {
                    using (var encryptor = aes.CreateEncryptor())
                    {
                        return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
                    }
                }
            }
        }


        /// <summary>
        ///  解密
        /// </summary>
        /// <param name="encryptedBytes">被加密的byte数组</param>
        /// <returns>解密后的byte数组</returns>
        public byte[] Decrypt(byte[] encryptedBytes)
        {
            Throws.ArgumentNullException(encryptedBytes, nameof(encryptedBytes));

            using (var aes = BuildAesAlgorithm())
            {
                //加密
                if (this.WithIV == true)
                {
                    using (var memoryStream = new MemoryStream(encryptedBytes))
                    {
                        var iv = new byte[16];
                        memoryStream.Read(iv, 0, iv.Length);
                        aes.IV = iv;

                        using (var decryptor = aes.CreateDecryptor())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var decryptedBytes = new byte[encryptedBytes.Length];
                                var byteCount = cryptoStream.Read(decryptedBytes, 0, encryptedBytes.Length);
                                return decryptedBytes.Take(byteCount).ToArray();
                            }
                        }
                    }
                }
                else
                {
                    using (var decryptor = aes.CreateDecryptor())
                    {
                        return decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    }
                }
            }
        }

        private AesCryptoServiceProvider BuildAesAlgorithm()
        {
            return new AesCryptoServiceProvider
            {
                Key = this.Key,
                Mode = this.Mode,
                Padding = this.Padding
            };
        }
    }
}
