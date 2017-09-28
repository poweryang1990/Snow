using System.Net;

namespace UOKOFramework
{
    /// <summary>
    /// IPHelper
    /// </summary>
    // ReSharper disable InconsistentNaming
    public class IPHelper
    {
        /// <summary>
        /// 解析ip字符串为IPAddress,解析失败返回null
        /// </summary>
        /// <param name="ip">ip字符串</param>
        /// <returns>IPAddress</returns>
        public IPAddress ToIPAddress(string ip)
        {
            if (ip == null)
            {
                return null;
            }
            IPAddress.TryParse(ip, out var ipAddress);
            return ipAddress;
        }

        /// <summary>
        /// 解析为IPv4,如果不是IPv4或者格式有误，则返回null
        /// </summary>
        /// <param name="ipv4Text">ipv4文本字符串</param>
        /// <returns></returns>
        public IPv4? ToIPv4(string ipv4Text)
        {
            var ipAddress = ToIPAddress(ipv4Text);
            return ToIPv4(ipAddress);
        }

        /// <summary>
        /// 转换为IPv4,如果不是IPv4返回null
        /// </summary>
        /// <param name="ipAddress">IPAddress</param>
        /// <returns></returns>
        public IPv4? ToIPv4(IPAddress ipAddress)
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
