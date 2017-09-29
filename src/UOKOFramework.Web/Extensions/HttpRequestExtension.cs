using System.Net;
using System.Web;
// ReSharper disable InconsistentNaming

namespace UOKOFramework.Web.Extensions
{
    /// <summary>
    /// HttpRequest的扩展方法
    /// </summary>
    public static class HttpRequestExtension
    {
        /// <summary>
        /// 获取当前请求的IP
        /// </summary>
        /// <param name="httpRequest">this</param>
        /// <returns>客户端的ip</returns>
        public static IPAddress GetCleintIP(this HttpRequest httpRequest)
        {
            return new IPHelper().GetCleintIP(httpRequest);
        }

        /// <summary>
        /// 获取当前请求的IP
        /// </summary>
        /// <param name="httpRequest">this</param>
        /// <returns>客户端的ip</returns>
        public static IPAddress GetCleintIP(this HttpRequestBase httpRequest)
        {
            return new IPHelper().GetCleintIP(httpRequest);
        }
    }
}
