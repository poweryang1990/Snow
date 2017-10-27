using System;
using System.Reflection;

namespace Snow.Extensions
{
    /// <summary>
    /// Assembly帮助类
    /// </summary>
    public static class AssemblyExtension
    {
        /// <summary>
        /// 读取Assembly中的资源
        /// </summary>
        /// <param name="assembly">this</param>
        /// <param name="predicate">First资源文件名表达式</param>
        /// <returns>bytes</returns>
        public static byte[] GetResourceBytes(this Assembly assembly, Func<string, bool> predicate)
        {
            return AssemblyHelper.New().GetResourceBytes(assembly, predicate);
        }

        /// <summary>
        /// 读取Assembly中的资源
        /// </summary>
        /// <param name="assembly">this</param>
        /// <param name="resourceFileName">资源文件名</param>
        /// <param name="isFullName">是否是完全限定名</param>
        /// <returns>bytes</returns>
        public static byte[] GetResourceBytes(this Assembly assembly, string resourceFileName, bool isFullName = false)
        {
            return AssemblyHelper.New().GetResourceBytes(assembly, resourceFileName, isFullName);
        }
    }
}
