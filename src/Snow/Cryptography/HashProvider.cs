using System.Security.Cryptography;

// ReSharper disable InconsistentNaming

namespace Snow.Cryptography
{

    /// <summary>
    /// 散列值提供者
    /// </summary>
    public sealed class HashProvider
    {
        /// <summary>
        /// 创建新的HashHelper对象
        /// </summary>
        /// <param name="bytes">原始byte数组</param>
        public static HashProvider New(byte[] bytes)
        {
            return new HashProvider
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
            get => _bytes;
            set
            {
                Throws.ArgumentNullException(value, nameof(value));
                _bytes = value;
            }
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
        /// 获取SHA384
        /// </summary>
        /// <returns>48byte的数组</returns>
        public byte[] GetSHA384()
        {
            return GetHashCore(new SHA384CryptoServiceProvider());
        }

        /// <summary>
        /// 获取SHA512
        /// </summary>
        /// <returns>64byte的数组</returns>
        public byte[] GetSHA512()
        {
            return GetHashCore(new SHA512CryptoServiceProvider());
        }

        #region 私有成员

        private byte[] GetHashCore(HashAlgorithm hashAlgorithm)
        {
            using (hashAlgorithm)
            {
                return hashAlgorithm.ComputeHash(Bytes);
            }
        }
        #endregion
    }
}
