using System.Security.Claims;

namespace Snow.Authentication
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