using System;

namespace Snow.Office.Excel
{
    /// <summary>
    /// ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Column<T>
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// ֵ�ı��ʽ
        /// </summary>
        public Func<T, object> ValueExpression { set; get; }

        /// <summary>
        /// ֵ����ʽ
        /// </summary>
        public string ValueStyle { set; get; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="valueExpression">ֵ�ı��ʽ</param>
        public Column(
            string name,
            Func<T, object> valueExpression)
        {
            Name = name;
            this.ValueExpression = valueExpression;
        }
    }
}