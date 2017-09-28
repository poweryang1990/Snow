using System;
using System.Reflection;

namespace UOKOFramework.Extensions
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
            return new AssemblyHelper().GetResourceBytes(assembly, predicate);
        }

        /// <summary>
        /// 读取Assembly中的资源
        /// </summary>
        /// <param name="assembly">this</param>
        /// <param name="resourceFullName">资源文件的完全限定名</param>
        /// <returns>bytes</returns>
        public static byte[] GetResourceBytes(this Assembly assembly, string resourceFullName)
        {
            return new AssemblyHelper().GetResourceBytes(assembly, resourceFullName);
        }
    }
}
