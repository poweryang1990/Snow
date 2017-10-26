using System;
using System.Linq;
using System.Linq.Expressions;

namespace Snow.Extensions
{
    /// <summary>
    /// Quaryable的扩展方法
    /// </summary>
    public static class QuaryableExtension
    {
        /// <summary>
        /// 在condition为true的情况下应用Where表达式
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="source">this</param>
        /// <param name="condition">条件</param>
        /// <param name="predicate">Where表达式</param>
        /// <returns>IQueryable</returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
