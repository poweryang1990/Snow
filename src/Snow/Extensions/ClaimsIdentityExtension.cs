using System;
using System.Linq;
using System.Security.Claims;

namespace Snow.Extensions
{
    /// <summary>
    /// ClaimsIdentity的扩展
    /// </summary>
    public static class ClaimsIdentityExtension
    {
        /// <summary>
        /// 查找第一个匹配的Claim的Value
        /// </summary>
        /// <param name="claimsIdentity">ClaimsIdentity</param>
        /// <param name="claimType"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string FindFirstValue(
            this ClaimsIdentity claimsIdentity,
            string claimType,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return claimsIdentity
                ?.FindFirst(claim => string.Equals(claim.Type, claimType, comparison))
                ?.Value;
        }

        /// <summary>
        /// 查找所有匹配的Claim的Value
        /// </summary>
        /// <param name="claimsIdentity">ClaimsIdentity</param>
        /// <param name="claimType"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string[] FindAllValues(
            this ClaimsIdentity claimsIdentity,
            string claimType,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return claimsIdentity?
                .FindAll(claim => string.Equals(claim.Type, claimType, comparison))
                .Select(claim => claim.Value)
                .ToArray();
        }
    }
}