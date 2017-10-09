using System.Security.Claims;
using System.Web;
using Snow.Security;

namespace Snow.Web
{
    /// <summary>
    /// 来自HttpContext的Principal访问器
    /// </summary>
    public class HttpContextPrincipalAccessor : PrincipalAccessor
    {
        /// <summary>
        /// 获取Principal
        /// </summary>
        public override ClaimsPrincipal Principal
        {
            get
            {
                if (HttpContext.Current?.User is ClaimsPrincipal claimsPrincipal)
                {
                    return claimsPrincipal;
                }
                return base.Principal;
            }
        }
    }
}