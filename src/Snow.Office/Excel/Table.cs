using System.Collections.Generic;

namespace Snow.Office.Excel
{
    /// <summary>
    /// ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Table<T>
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// ��
        /// </summary>
        public List<Column<T>> Columns { get; } = new List<Column<T>>();

        /// <summary>
        /// ����Դ
        /// </summary>
        public ICollection<T> DataSource { get; set; }
    }
}