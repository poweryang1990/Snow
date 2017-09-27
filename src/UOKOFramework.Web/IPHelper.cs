using System.Collections.Specialized;
using System.Net;
using System.Web;

namespace UOKOFramework.Web
{
    /// <summary>
    /// IPHelper
    /// </summary>
    // ReSharper disable InconsistentNaming
    public class IPHelper
    {
        ///  <summary>    
        ///  获取当前请求的IP
        ///  </summary>    
        public IPAddress GetCleintIP(HttpRequest httpRequest)
        {
            var ip = GetIpFromServerVariables(httpRequest.ServerVariables);

            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = httpRequest.UserHostAddress;
            }
            return this.Parse(ip);
        }

        ///  <summary>    
        ///  获取当前请求的IP
        ///  </summary>    
        public IPAddress GetCleintIP(HttpRequestBase httpRequest)
        {
            var ip = GetIpFromServerVariables(httpRequest.ServerVariables);

            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = httpRequest.UserHostAddress;
            }
            return this.Parse(ip);
        }

        /// <summary>
        /// 解析ip字符串为IPAddress,解析失败返回null
        /// </summary>
        /// <param name="ip">ip字符串</param>
        /// <returns>IPAddress</returns>
        public IPAddress Parse(string ip)
        {
            if (ip == null)
            {
                return null;
            }
            IPAddress.TryParse(ip, out var ipAddress);
            return ipAddress;
        }

        private string GetIpFromServerVariables(NameValueCollection serverVariables)
        {
            var ip = serverVariables["HTTP_X_FORWARDED_FOR"];
            ip = ip?.Split(',')[0];
            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = serverVariables["HTTP_X_REAL_IP"];
            }
            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = serverVariables["REMOTE_ADDR"];
            }
            return ip;
        }
    }
}
