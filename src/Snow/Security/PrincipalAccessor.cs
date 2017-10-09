using System.Security.Claims;
using System.Threading;

namespace Snow.Security
{
    /// <summary>
    /// Principal访问器
    /// </summary>
    public class PrincipalAccessor : IPrincipalAccessor
    {
        /// <summary>
        /// 获取Principal
        /// </summary>
        public virtual ClaimsPrincipal Principal
        {
            get
            {
                var claimsPrincipal = ClaimsPrincipal.Current;
                if (claimsPrincipal != null)
                {
                    return claimsPrincipal;
                }
                return Thread.CurrentPrincipal as ClaimsPrincipal;
            }
        }
    }
}