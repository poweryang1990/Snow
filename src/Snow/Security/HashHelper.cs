using System.Security.Cryptography;

// ReSharper disable InconsistentNaming

namespace Snow.Security
{

    /// <summary>
    /// 散列值帮助类
    /// </summary>
    public class HashHelper
    {
        /// <summary>
        /// 创建新的HashHelper对象
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        public static HashHelper New(byte[] bytes)
        {
            return new HashHelper
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
        /// 获取散列值
        /// </summary>
        /// <param name="hashAlgorithm">散列算法</param>
        /// <returns></returns>
        public byte[] GetHash(HashAlgorithm hashAlgorithm)
        {
            Throws.ArgumentNullException(hashAlgorithm, nameof(hashAlgorithm));
            return GetHashCore(hashAlgorithm);
        }

        /// <summary>
        /// 获取MD5
        /// </summary>
        /// <returns>16byte的数组</returns>
        public byte[] GetMD5()
        {
            return GetHashCore(new MD5CryptoServiceProvider());
        }

        /// <summary>
        /// 获取SHA1
        /// </summary>
        /// <returns>20byte的数组</returns>
        public byte[] GetSHA1()
        {
            return GetHashCore(new SHA1CryptoServiceProvider());
        }

        /// <summary>
        /// 获取SHA256
        /// </summary>
        /// <returns>32byte的数组</returns>
        public byte[] GetSHA256()
        {
            return GetHashCore(new SHA256CryptoServiceProvider());
        }

        /// <summary>
        /// 获取基于MD5的MAC
        /// </summary>
        /// <param name="key">MAC的密钥</param>
        /// <returns>16byte的数组</returns>
        public byte[] GetHMACMD5(byte[] key)
        {
            Throws.ArgumentNullException(key, nameof(key));
            return GetHashCore(new HMACMD5(key));
        }

        /// <summary>
        /// 获取基于SHA1的MAC
        /// </summary>
        /// <param name="key">MAC的密钥</param>
        /// <returns>20byte的数组</returns>
        public byte[] GetHMACSHA1(byte[] key)
        {
            Throws.ArgumentNullException(key, nameof(key));
            return GetHashCore(new HMACSHA1(key));
        }

        #region 私有成员

        private byte[] GetHashCore(HashAlgorithm hashAlgorithm)
        {
            using (hashAlgorithm)
            {
                return hashAlgorithm.ComputeHash(this.Bytes);
            }
        }
        #endregion
    }
}
