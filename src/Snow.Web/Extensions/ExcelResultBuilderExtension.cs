using System.Collections.Generic;
using Snow.Web;

// ReSharper disable CheckNamespace
namespace Snow.Extensions
{
    /// <summary>
    /// Excel的扩展方法
    /// </summary>
    public static class ExcelResultBuilderExtension
    {
        /// <summary>
        /// 转换成ExcelResultBuilder
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="dataSouce">数据源</param>
        /// <param name="title">Table标题</param>
        /// <returns></returns>
        public static ExcelResultBuilder<T> NewExcelResultBuilder<T>(this ICollection<T> dataSouce, string title)
        {
            return ExcelResultBuilder<T>
                .New()
                .SetTitle(title)
                .SetDataSource(dataSouce);
        }
    }
}
