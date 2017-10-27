using System.Security.Cryptography;

// ReSharper disable InconsistentNaming

namespace Snow.Cryptography
{

    /// <summary>
    /// 消息认证码提供者
    /// </summary>
    public sealed class MACProvider
    {
        /// <summary>
        /// 创建新的MACHelper对象
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        public static MACProvider New(byte[] bytes)
        {
            return new MACProvider
            {
                Bytes = bytes
            };
        }

        private byte[] _bytes;

        /// <summary>
        /// 获取原始byte数组
        /// </summary>
        public byte[] Bytes
        {
            get => this._bytes;
            set
            {
                Throws.ArgumentNullException(value, nameof(value));
                this._bytes = value;
            }
        }

        /// <summary>
        /// 获取基于MD5的MAC
        /// </summary>
        /// <param name="key">MAC的密钥</param>
        /// <returns>16byte的数组</returns>
        public byte[] GetHMACMD5(byte[] key)
        {
            Throws.ArgumentNullException(key, nameof(key));
            return GetMACCore(new HMACMD5(key));
        }

        /// <summary>
        /// 获取基于SHA1的MAC
        /// </summary>
        /// <param name="key">MAC的密钥</param>
        /// <returns>20byte的数组</returns>
        public byte[] GetHMACSHA1(byte[] key)
        {
            Throws.ArgumentNullException(key, nameof(key));
            return GetMACCore(new HMACSHA1(key));
        }

        /// <summary>
        /// 获取基于SHA256的MAC
        /// </summary>
        /// <param name="key">MAC的密钥</param>
        /// <returns>20byte的数组</returns>
        public byte[] GetHMACSHA256(byte[] key)
        {
            Throws.ArgumentNullException(key, nameof(key));
            return GetMACCore(new HMACSHA256(key));
        }

        #region 私有成员

        private byte[] GetMACCore(HashAlgorithm keyedHashAlgorithm)
        {
            using (keyedHashAlgorithm)
            {
                return keyedHashAlgorithm.ComputeHash(this.Bytes);
            }
        }
        #endregion
    }
}
