using System.Security.Claims;

namespace UOKOFramework.Security
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