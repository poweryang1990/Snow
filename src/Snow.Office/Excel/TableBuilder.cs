using System;
using System.Collections.Generic;

namespace Snow.Office.Excel
{
    /// <summary>
    /// 表格Builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TableBuilder<T>
    {
        private readonly Table<T> _table = new Table<T>();

        /// <summary>
        /// 设置表格名
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public TableBuilder<T> SetTitle(string title)
        {
            this._table.Title = title;
            return this;
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public TableBuilder<T> SetDataSource(ICollection<T> dataSource)
        {
            this._table.DataSource = dataSource;
            return this;
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="valueExpression">值的表达式</param>
        /// <returns></returns>
        public TableBuilder<T> AddColumn(string name, Func<T, object> valueExpression)
        {
            AddColumnCore(name, valueExpression, string.Empty);
            return this;
        }

        /// <summary>
        /// 添加文本列
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="valueExpression">值的表达式</param>
        /// <returns></returns>
        public TableBuilder<T> AddTextColumn(string name, Func<T, object> valueExpression)
        {
            AddColumnCore(name, valueExpression, "vnd.ms-excel.numberformat:@");
            return this;
        }

        private void AddColumnCore(string name, Func<T, object> valueExpression, string valueStyle)
        {
            var column = new Column<T>(name, valueExpression)
            {
                ValueStyle = valueStyle
            };

            this._table.Columns.Add(column);
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        internal Table<T> Build()
        {
            return _table;
        }

        /// <summary>
        /// New
        /// </summary>
        /// <returns></returns>
        public static TableBuilder<T> New()
        {
            return new TableBuilder<T>();
        }
    }
}