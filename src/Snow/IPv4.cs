using System.Net;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming
namespace Snow
{
    /// <summary>
    /// IPv4
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct IPv4
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="raw"></param>
        public IPv4(int raw)
        {
            this._byte1 = 0;
            this._byte2 = 0;
            this._byte3 = 0;
            this._byte4 = 0;
            this._raw = raw;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="byte1"></param>
        /// <param name="byte2"></param>
        /// <param name="byte3"></param>
        /// <param name="byte4"></param>
        public IPv4(byte byte1, byte byte2, byte byte3, byte byte4)
        {
            this._raw = 0;
            this._byte1 = byte1;
            this._byte2 = byte2;
            this._byte3 = byte3;
            this._byte4 = byte4;
        }

        [FieldOffset(0)]
        private int _raw;

        [FieldOffset(0)]
        private byte _byte1;

        [FieldOffset(1)]
        private byte _byte2;

        [FieldOffset(2)]
        private byte _byte3;

        [FieldOffset(3)]
        private byte _byte4;

        /// <summary>
        /// 原始数据
        /// </summary>
        public int Raw
        {
            get => this._raw;
            set => this._raw = value;
        }

        /// <summary>
        /// Byte1
        /// </summary>
        public byte Byte1
        {
            get => this._byte1;
            set => this._byte1 = value;
        }

        /// <summary>
        /// Byte2
        /// </summary>
        public byte Byte2
        {
            get => this._byte2;
            set => this._byte2 = value;
        }

        /// <summary>
        /// Byte3
        /// </summary>
        public byte Byte3
        {
            get => this._byte3;
            set => this._byte3 = value;
        }

        /// <summary>
        /// Byte4
        /// </summary>
        public byte Byte4
        {
            get => this._byte4;
            set => this._byte4 = value;
        }

        /// <summary>
        /// Bytes
        /// </summary>
        public byte[] Bytes => new[] { this._byte1, this._byte2, this._byte3, this._byte4 };

        /// <summary>
        /// 显示字符串格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this._byte1}.{this._byte2}.{this._byte3}.{this._byte4}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IPv4 other)
        {
            return _raw == other._raw;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return obj is IPv4 && Equals((IPv4)obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this._raw.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator IPv4(int value)
        {
            return new IPv4(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipv4"></param>
        public static explicit operator int(IPv4 ipv4)
        {
            return ipv4._raw;
        }

        /// <summary>
        /// 重载==
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(IPv4 x, IPv4 y)
        {
            return x._raw == y._raw;
        }

        /// <summary>
        /// 重载！=
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(IPv4 x, IPv4 y)
        {
            return !(x == y);
        }
    }
}
