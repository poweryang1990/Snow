using Snow.Office.Excel;

// ReSharper disable CheckNamespace
namespace Snow.Extensions
{
    /// <summary>
    /// TableBuilder的扩展方法
    /// </summary>
    public static class TableBuilderExtension
    {
        /// <summary>
        /// 生成Html格式的Excel。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableBuilder">this</param>
        /// <returns></returns>
        public static string BuildHtmlExcel<T>(this TableBuilder<T> tableBuilder)
        {
            var table = tableBuilder.Build();
            var tableToHtmlExcel = new TableToHtmlExcel<T>(table);
            return tableToHtmlExcel.BuildExcel();
        }
    }
}
