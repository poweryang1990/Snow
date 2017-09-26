using System;
using System.Linq;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// Array的扩展方法
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// 合并数组
        /// </summary>
        /// <param name="current">当前数组</param>
        /// <param name="nexts">下一个数组</param>
        /// <returns>合并后的数组</returns>
        public static T[] Combine<T>(this T[] current, params T[][] nexts)
        {
            Throws.ArgumentNullException(current, nameof(current));
            Throws.ArgumentNullException(nexts, nameof(nexts), true);

            var nextsLength = nexts.Sum(_ => _.Length);
            var result = new T[current.Length + nextsLength];

            var offset = 0;
            Buffer.BlockCopy(current, 0, result, offset, current.Length);

            offset = current.Length;
            foreach (var next in nexts)
            {
                Buffer.BlockCopy(next, 0, result, offset, next.Length);
                offset = offset + next.Length;
            }

            return result;
        }
    }
}
