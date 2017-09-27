using System.Net;
using System.Web;
// ReSharper disable InconsistentNaming

namespace UOKOFramework.Web.Extensions
{
    /// <summary>
    /// HttpContext的扩展方法
    /// </summary>
    public static class HttpContextExtension
    {
        /// <summary>
        /// 获取当前请求的IP
        /// </summary>
        /// <param name="httpContext">this</param>
        /// <returns>客户端的ip</returns>
        public static IPAddress GetCleintIP(this HttpContext httpContext)
        {
            return new IPHelper().GetCleintIP(httpContext?.Request);
        }

        /// <summary>
        /// 获取当前请求的IP
        /// </summary>
        /// <param name="httpContext">this</param>
        /// <returns>客户端的ip</returns>
        public static IPAddress GetCleintIP(this HttpContextBase httpContext)
        {
            return new IPHelper().GetCleintIP(httpContext.Request);
        }
    }
}
