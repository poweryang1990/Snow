using System.Security.Claims;

namespace Snow.Security
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