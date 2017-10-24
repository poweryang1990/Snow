using System;
using System.Collections.Generic;
using Snow.Extensions;
using Snow.Office.Excel;

// ReSharper disable CheckNamespace
namespace Snow.Web
{
    /// <summary>
    /// Excel的扩展方法
    /// </summary>
    public class ExcelResultBuilder<T>
    {
        private readonly TableBuilder<T> _tableBuilder;

        private ExcelResultBuilder()
        {
            _tableBuilder = new TableBuilder<T>();
        }

        /// <summary>
        /// new
        /// </summary>
        /// <returns></returns>
        public static ExcelResultBuilder<T> New()
        {
            return new ExcelResultBuilder<T>();
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public ExcelResultBuilder<T> SetTitle(string title)
        {
            _tableBuilder.SetTitle(title);
            return this;
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="dataSouce"></param>
        /// <returns></returns>
        public ExcelResultBuilder<T> SetDataSource(ICollection<T> dataSouce)
        {
            _tableBuilder.SetDataSource(dataSouce);
            return this;
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="valueExpression">值的表达式</param>
        /// <returns></returns>
        public ExcelResultBuilder<T> AddColumn(string name, Func<T, object> valueExpression)
        {
            _tableBuilder.AddColumn(name, valueExpression);
            return this;
        }

        /// <summary>
        /// 添加文本列
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="valueExpression">值的表达式</param>
        /// <returns></returns>
        public ExcelResultBuilder<T> AddTextColumn(string name, Func<T, object> valueExpression)
        {
            _tableBuilder.AddTextColumn(name, valueExpression);
            return this;
        }


        /// <summary>
        /// 构建为ExcelResult
        /// </summary>
        /// <returns></returns>
        public ExcelResult Build(string fileName)
        {
            var excelString = _tableBuilder.BuildHtmlExcel();
            var excelBytes = excelString.GetBytes();
            return new ExcelResult(fileName, excelBytes);
        }

    }
}
