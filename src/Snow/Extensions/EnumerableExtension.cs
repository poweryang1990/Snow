using System;
using System.Collections.Generic;
using System.Linq;

namespace Snow.Extensions
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
        /// <param name="source">this</param>
        /// <param name="condition">条件</param>
        /// <param name="predicate">Where表达式</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source,
            bool condition,
            Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 将字符串连接在一起
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string JoinString(this IEnumerable<string> source, string separator)
        {
            if (source == null)
            {
                return string.Empty;
            }

            return string.Join(separator, source);
        }

        /// <summary>
        /// 将字符串连接在一起
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="source"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="selector">映射器</param>
        /// <returns></returns>
        public static string JoinString<T>(this IEnumerable<T> source,
            string separator, Func<T, string> selector)
        {
            if (source == null)
            {
                return string.Empty;
            }

            return source.Select(selector).JoinString(separator);
        }

        /// <summary>
        /// 将字符串连接在一起
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string JoinString<T>(this IEnumerable<T> source, string separator)
        {
            if (source == null)
            {
                return string.Empty;
            }

            return source.Select(x => x.ToString()).JoinString(separator);
        }

        /// <summary>
        /// CastArray
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">this</param>
        /// <returns>Array</returns>
        public static TTarget[] CastArray<TSource, TTarget>(this IEnumerable<TSource> source)
        {
            return source?.Cast<TTarget>().ToArray();
        }

        /// <summary>
        /// CastList
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">this</param>
        /// <returns>List</returns>
        public static List<TTarget> CastList<TSource, TTarget>(this IEnumerable<TSource> source)
        {
            return source?.Cast<TTarget>().ToList();
        }
    }
}