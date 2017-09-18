using System;
using System.Linq;
using System.Linq.Expressions;

namespace UOKOFramework.Extensions
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
        /// <param name="queryable">this</param>
        /// <param name="condition">条件</param>
        /// <param name="predicate">Where表达式</param>
        /// <returns>IQueryable</returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? queryable.Where(predicate) : queryable;
        }
    }
}
