using Snow.Web;
using System.Net;
using System.Web;
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
namespace Snow.Extensions
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
        public static IPAddress GetClientIP(this HttpContext httpContext)
        {
            return new IPHelper().GetClientIP(httpContext?.Request);
        }

        /// <summary>
        /// 获取当前请求的IP
        /// </summary>
        /// <param name="httpContext">this</param>
        /// <returns>客户端的ip</returns>
        public static IPAddress GetClientIP(this HttpContextBase httpContext)
        {
            return new IPHelper().GetClientIP(httpContext?.Request);
        }
    }
}
