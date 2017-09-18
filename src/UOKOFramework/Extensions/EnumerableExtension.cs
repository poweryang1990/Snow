using System;
using System.Collections.Generic;
using System.Linq;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// Enumerable的扩展方法
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// 在condition为true的情况下应用Where表达式
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="queryable">this</param>
        /// <param name="condition">条件</param>
        /// <param name="predicate">Where表达式</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> queryable,
            bool condition,
            Func<T, bool> predicate)
        {
            return condition ? queryable.Where(predicate) : queryable;
        }
    }
}