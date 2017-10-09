using Microsoft.Owin;

// ReSharper disable CheckNamespace
namespace Snow.Extensions
{
    /// <summary>
    /// IOwinRequest的扩展方法
    /// </summary>
    public static class OwinRequestExtension
    {
        private const string XRequestedWithHeaderName = "X-Requested-With";
        private const string XRequestedWithHeaderValue = "XMLHttpRequest";
        private const string UserAgentHeaderName = "User-Agent";

        /// <summary>
        /// 是否是Ajax请求
        /// </summary>
        /// <param name="request">this</param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this IOwinRequest request)
        {
            if (request == null)
            {
                return false;
            }

            var query = request.Query;

            if (query != null && query[XRequestedWithHeaderName] == XRequestedWithHeaderValue)
            {
                return true;
            }

            var headers = request.Headers;
            if (headers != null && headers[XRequestedWithHeaderName] == XRequestedWithHeaderValue)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 是否是Bearer Authorization
        /// </summary>
        /// <param name="request">this</param>
        /// <returns></returns>
        public static bool IsBearerAuthorization(this IOwinRequest request)
        {
            if (request == null)
            {
                return false;
            }
            var authorization = request.Headers.Get("Authorization");
            return authorization != null && authorization.StartsWith("Bearer ");
        }

        /// <summary>
        /// 请求是否来自WeChat客户端
        /// </summary>
        /// <param name="request">this</param>
        /// <returns></returns>
        public static bool IsFromWeChat(this IOwinRequest request)
        {
            if (request == null)
            {
                return false;
            }

            return request.Headers.Get(UserAgentHeaderName)
                .Include("micromessenger");
        }

        /// <summary>
        /// 请求是否来自AliPay客户端
        /// </summary>
        /// <param name="request">this</param>
        /// <returns></returns>
        public static bool IsFromAliPay(this IOwinRequest request)
        {
            if (request == null)
            {
                return false;
            }

            return request.Headers.Get(UserAgentHeaderName)
                .Include("alipay");
        }

        /// <summary>
        /// 请求是否来自京东客户端
        /// </summary>
        /// <param name="request">this</param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsFromJD(this IOwinRequest request)
        {
            if (request == null)
            {
                return false;
            }

            return request.Headers.Get(UserAgentHeaderName)
                .Include("jd");
        }
    }
}
