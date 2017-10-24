using System;
using System.Collections.Generic;

namespace Snow.Office.Excel
{
    /// <summary>
    /// ���Builder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TableBuilder<T>
    {
        private readonly Table<T> _table = new Table<T>();

        /// <summary>
        /// ���ñ����
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public TableBuilder<T> SetTitle(string title)
        {
            this._table.Title = title;
            return this;
        }

        /// <summary>
        /// ��������Դ
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public TableBuilder<T> SetDataSource(ICollection<T> dataSource)
        {
            this._table.DataSource = dataSource;
            return this;
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="valueExpression">ֵ�ı��ʽ</param>
        /// <returns></returns>
        public TableBuilder<T> AddColumn(string name, Func<T, object> valueExpression)
        {
            AddColumnCore(name, valueExpression, string.Empty);
            return this;
        }

        /// <summary>
        /// ����ı���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="valueExpression">ֵ�ı��ʽ</param>
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
        /// ����
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