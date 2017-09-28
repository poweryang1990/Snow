using System.Collections.Specialized;
using System.Net;
using System.Web;

namespace UOKOFramework.Web
{
    /// <summary>
    /// IPHelper
    /// </summary>
    // ReSharper disable InconsistentNaming
    public class WebHelper
    {
        ///  <summary>    
        ///  获取当前请求的IP
        ///  </summary>    
        public IPAddress GetCleintIP(HttpRequest httpRequest)
        {
            if (httpRequest == null)
            {
                return null;
            }

            var ip = GetIpFromServerVariables(httpRequest.ServerVariables);

            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = httpRequest.UserHostAddress;
            }
            return new IPHelper().ToIPAddress(ip);
        }

        ///  <summary>    
        ///  获取当前请求的IP
        ///  </summary>    
        public IPAddress GetCleintIP(HttpRequestBase httpRequest)
        {
            if (httpRequest == null)
            {
                return null;
            }

            var ip = GetIpFromServerVariables(httpRequest.ServerVariables);

            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = httpRequest.UserHostAddress;
            }
            return new IPHelper().ToIPAddress(ip);
        }

        private string GetIpFromServerVariables(NameValueCollection serverVariables)
        {
            if (serverVariables == null)
            {
                return null;
            }

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
