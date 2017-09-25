using System;
using System.Linq;
using System.Security.Claims;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// ClaimsPrincipal的扩展
    /// </summary>
    public static class ClaimsPrincipalExtension
    {
        /// <summary>
        /// 查找第一个匹配的Claim的Value
        /// </summary>
        /// <param name="claimsPrincipal">ClaimsPrincipal</param>
        /// <param name="claimType"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string FindFirstValue(
            this ClaimsPrincipal claimsPrincipal,
            string claimType,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return claimsPrincipal
                ?.FindFirst(claim => string.Equals(claim.Type, claimType, comparison))
                ?.Value;
        }

        /// <summary>
        /// 查找所有匹配的Claim的Value
        /// </summary>
        /// <param name="claimsPrincipal">ClaimsPrincipal</param>
        /// <param name="claimType"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string[] FindAllValues(
            this ClaimsPrincipal claimsPrincipal,
            string claimType,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return claimsPrincipal?
                .FindAll(claim => string.Equals(claim.Type, claimType, comparison))
                .Select(claim => claim.Value)
                .ToArray();
        }
    }
}