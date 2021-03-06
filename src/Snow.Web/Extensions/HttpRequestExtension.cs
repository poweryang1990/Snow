﻿using System.Net;
using System.Web;
using Snow.Web;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
namespace Snow.Extensions
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
        public static IPAddress GetClientIP(this HttpRequest httpRequest)
        {
            return new IPHelper().GetClientIP(httpRequest);
        }

        /// <summary>
        /// 获取当前请求的IP
        /// </summary>
        /// <param name="httpRequest">this</param>
        /// <returns>客户端的ip</returns>
        public static IPAddress GetClientIP(this HttpRequestBase httpRequest)
        {
            return new IPHelper().GetClientIP(httpRequest);
        }
    }
}
