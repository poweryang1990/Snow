using System.Security.Claims;

namespace Snow.Security
{
    /// <summary>
    /// Principal������
    /// </summary>
    public interface IPrincipalAccessor
    {
        /// <summary>
        /// ��ȡPrincipal
        /// </summary>
        ClaimsPrincipal Principal { get; }
    }
}