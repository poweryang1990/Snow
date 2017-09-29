using System.Net;

namespace UOKOFramework
{
    /// <summary>
    /// IPConverter
    /// </summary>
    // ReSharper disable InconsistentNaming
    public static class IPConverter
    {
        /// <summary>
        /// 解析ip字符串为IPAddress,解析失败返回null
        /// </summary>
        /// <param name="ip">ip字符串</param>
        /// <returns>IPAddress</returns>
        public static IPAddress ToIPAddress(string ip)
        {
            if (ip == null)
            {
                return null;
            }
            IPAddress.TryParse(ip, out var ipAddress);
            return ipAddress;
        }

        /// <summary>
        /// 解析为IPv4,如果不是IPv4返回null
        /// </summary>
        /// <param name="ipv4Text">ipv4文本字符串</param>
        /// <returns></returns>
        public static IPv4? ToIPv4(string ipv4Text)
        {
            return ToIPv4(ToIPAddress(ipv4Text));
        }

        /// <summary>
        /// 转换为IPv4,如果不是IPv4返回null
        /// </summary>
        /// <param name="ipAddress">IPAddress</param>
        /// <returns></returns>
        public static IPv4? ToIPv4(IPAddress ipAddress)
        {
            var ipBytes = ipAddress?.GetAddressBytes();
            if (ipBytes?.Length != 4)
            {
                return null;
            }
            return new IPv4(ipBytes[0], ipBytes[1], ipBytes[2], ipBytes[3]);
        }
    }
}
