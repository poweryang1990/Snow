using System.Security.Claims;

namespace Snow.Security
{
    /// <summary>
    /// Identity访问器
    /// </summary>
    public class IdentityAccessor : IIdentityAccessor
    {
        private readonly IPrincipalAccessor _principalAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="principalAccessor">Principal访问器</param>
        public IdentityAccessor(IPrincipalAccessor principalAccessor)
        {
            Throws.ArgumentNullException(principalAccessor, nameof(principalAccessor));
            this._principalAccessor = principalAccessor;
        }

        /// <summary>
        /// 获取Identity
        /// </summary>
        public virtual ClaimsIdentity Identity => this._principalAccessor.Principal?.Identity as ClaimsIdentity;
    }
}