using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming
namespace UOKOFramework
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
        public IPv4(uint raw)
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
        private uint _raw;

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
        public uint Raw
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
        /// <param name="value"></param>
        public static implicit operator IPv4(uint value)
        {
            return new IPv4(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipv4"></param>
        public static explicit operator uint(IPv4 ipv4)
        {
            return ipv4._raw;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipv4"></param>
        public static explicit operator byte[] (IPv4 ipv4)
        {
            return new byte[] { ipv4._byte1, ipv4._byte2, ipv4._byte3, ipv4._byte4 };
        }
    }
}
