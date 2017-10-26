using System;

namespace Snow.Office.Excel
{
    /// <summary>
    /// 列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Column<T>
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 值的表达式
        /// </summary>
        public Func<T, object> ValueExpression { set; get; }

        /// <summary>
        /// 值的样式
        /// </summary>
        public string ValueStyle { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="valueExpression">值的表达式</param>
        public Column(
            string name,
            Func<T, object> valueExpression)
        {
            Name = name;
            this.ValueExpression = valueExpression;
        }
    }
}