using System.Security.Claims;

namespace UOKOFramework.Security
{
    /// <summary>
    /// Identity访问器
    /// </summary>
    public interface IIdentityAccessor
    {
        /// <summary>
        /// 获取Identity
        /// </summary>
        ClaimsIdentity Identity { get; }
    }
}