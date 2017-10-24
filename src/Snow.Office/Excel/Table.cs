using System.Collections.Generic;

namespace Snow.Office.Excel
{
    /// <summary>
    /// 表格
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Table<T>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// 列
        /// </summary>
        public List<Column<T>> Columns { get; } = new List<Column<T>>();

        /// <summary>
        /// 数据源
        /// </summary>
        public ICollection<T> DataSource { get; set; }
    }
}