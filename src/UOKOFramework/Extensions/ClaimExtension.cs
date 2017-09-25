using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// Claim的扩展
    /// </summary>
    public static class ClaimExtension
    {
        /// <summary>
        /// 查找第一个匹配的Claim的Value
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="claimType"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string FindFirstValue(
            this IEnumerable<Claim> claims,
            string claimType,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return claims
                ?.FirstOrDefault(claim => string.Equals(claim.Type, claimType, comparison))
                ?.Value;
        }

        /// <summary>
        /// 查找第一个匹配的Claim的Value
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="claimType"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string[] FindAllValues(
            this IEnumerable<Claim> claims,
            string claimType,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return claims
                 ?.Where(claim => string.Equals(claim.Type, claimType, comparison))
                .Select(claim => claim.Value)
                .ToArray();
        }
    }
}