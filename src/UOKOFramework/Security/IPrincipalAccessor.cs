using System.Security.Claims;

namespace UOKOFramework.Security
{
    /// <summary>
    /// Principal∑√Œ ∆˜
    /// </summary>
    public interface IPrincipalAccessor
    {
        /// <summary>
        /// ªÒ»°Principal
        /// </summary>
        ClaimsPrincipal Principal { get; }
    }
}