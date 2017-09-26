using System;

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
        /// <param name="next">下一个数组</param>
        /// <returns>合并后的数组</returns>
        public static T[] Combine<T>(this T[] current, T[] next)
        {
            Throws.ArgumentNullException(current, nameof(current));
            Throws.ArgumentNullException(next, nameof(next));

            var result = new T[current.Length + next.Length];

            Buffer.BlockCopy(current, 0, result, 0, current.Length);
            Buffer.BlockCopy(next, 0, result, current.Length, next.Length);

            return result;
        }
    }
}
