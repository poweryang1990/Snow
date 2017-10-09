using System;
using System.Security.Cryptography;
// ReSharper disable InconsistentNaming

namespace Snow.Security
{
    /// <summary>
    /// AES帮助类
    /// </summary>
    public class AESHelper
    {
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
        /// 构造函数
        /// </summary>
        /// <param name="key">key：长度只能是[16,24,32]</param>
        public AESHelper(byte[] key)
        {
            this.Key = key;
        }

        /// <summary>
        ///  加密[ECB,Zeros]
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        /// <returns>加密后的byte数组</returns>
        public byte[] Encrypt(byte[] bytes)
        {
            Throws.ArgumentNullException(bytes, nameof(bytes));

            using (var symmetricAlgorithm = new AesCryptoServiceProvider())
            {
                symmetricAlgorithm.Key = this.Key;
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
        ///  解密[ECB,Zeros]
        /// </summary>
        /// <param name="encryptedBytes">被加密的byte数组</param>
        /// <returns>解密后的byte数组</returns>
        public byte[] Decrypt(byte[] encryptedBytes)
        {
            Throws.ArgumentNullException(encryptedBytes, nameof(encryptedBytes));

            using (var symmetricAlgorithm = new AesCryptoServiceProvider())
            {
                symmetricAlgorithm.Key = this.Key;
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
