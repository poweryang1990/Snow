using System;

// ReSharper disable InconsistentNaming

namespace UOKOFramework.IPDB
{
    /// <summary>
    /// 位置信息
    /// </summary>
    public class Location
    {
        /// <summary>
        /// 获取IP
        /// </summary>
        public IPv4 IP { get; internal set; }

        /// <summary>
        /// 获取国家
        /// </summary>
        public string Country { get; internal set; }

        /// <summary>
        /// 获取省份
        /// </summary>
        public string State { get; internal set; }

        /// <summary>
        /// 获取城市
        /// </summary>
        public string City { get; internal set; }

        /// <summary>
        /// ToString
        /// <para>格式：IP,Country,State,City</para>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.IP + ","
                + this.Country + ","
                + this.State + ","
                + this.City;
        }
    }
}
