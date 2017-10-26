using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Snow
{
    /// <summary>
    /// Assembly帮助类
    /// </summary>
    public class AssemblyHelper
    {
        /// <summary>
        /// 创建新的AssemblyHelper对象
        /// </summary>
        /// <returns></returns>
        public static AssemblyHelper New()
        {
            return new AssemblyHelper();
        }

        /// <summary>
        /// 读取Assembly中的资源
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <param name="predicate">First资源文件名表达式</param>
        /// <returns>bytes</returns>
        public byte[] GetResourceBytes(Assembly assembly, Func<string, bool> predicate)
        {
            Throws.ArgumentNullException(assembly, nameof(assembly));
            Throws.ArgumentNullException(predicate, nameof(predicate));

            var resourceFullName = assembly.GetManifestResourceNames()
                .First(predicate);

            return GetResourceBytes(assembly, resourceFullName);
        }

        /// <summary>
        /// 读取Assembly中的资源
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <param name="resourceFileName">资源文件名</param>
        /// <param name="isFullName">是否是完全限定名</param>
        /// <returns>bytes</returns>
        public byte[] GetResourceBytes(Assembly assembly, string resourceFileName, bool isFullName)
        {
            Throws.ArgumentNullException(assembly, nameof(assembly));
            Throws.ArgumentNullException(resourceFileName, nameof(resourceFileName));

            if (isFullName == true)
            {
                return GetResourceBytes(assembly, resourceFileName);
            }

            return GetResourceBytes(assembly, _ => _.EndsWith(resourceFileName));
        }

        private byte[] GetResourceBytes(Assembly assembly, string resourceFullName)
        {
            using (var resourceStream = assembly.GetManifestResourceStream(resourceFullName))
            {
                if (resourceStream == null)
                {
                    return null;
                }
                resourceStream.Seek(0, SeekOrigin.Begin);
                var result = new byte[resourceStream.Length];
                resourceStream.Read(result, 0, result.Length);
                return result;
            }
        }
    }
}
