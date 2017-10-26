using System.Text;

namespace Snow.Office.Excel
{
    /// <summary>
    /// Table×ªExcel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TableToHtmlExcel<T>
    {
        private readonly Table<T> _table;

        /// <summary>
        /// table
        /// </summary>
        /// <param name="table"></param>
        public TableToHtmlExcel(Table<T> table)
        {
            _table = table;
        }

        /// <summary>
        /// ¹¹ÔìExcel
        /// </summary>
        /// <returns></returns>
        public string BuildExcel()
        {
            var capacity = this._table.DataSource.Count * 1024;

            var html = new StringBuilder(capacity);

            html.Append("<div>");
            html.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-style:Solid;font-family:Verdana;border-collapse:collapse;\">");
            AppendTitle(html);
            AppendColumnHeader(html);
            AppendColumns(html);
            html.Append("</table>");
            html.Append("</div>");

            return html.ToString();
        }

        private void AppendTitle(StringBuilder html)
        {
            if (string.IsNullOrEmpty(this._table.Title))
            {
                return;
            }
            html.AppendFormat("<tr><th colspan={0} align=\"center\" style=\"color:Black;font-size:10pt;font-weight:bold;\">{1}</th></tr>",
                    this._table.Columns.Count,
                    this._table.Title);
        }

        private void AppendColumnHeader(StringBuilder html)
        {
            html.Append("<tr align=\"center\" style=\"color:Black;font-size:10pt;font-weight:bold;\">");

            foreach (var column in this._table.Columns)
            {
                html.AppendFormat("<th align=\"center\" style=\"background-color:#00FFFF\">{0}</th>", column.Name);
            }

            html.Append("</tr>");
        }

        private void AppendColumns(StringBuilder html)
        {
            foreach (var data in this._table.DataSource)
            {
                html.Append("<tr align=\"center\">");
                foreach (var column in this._table.Columns)
                {
                    var style = string.Empty;
                    if (string.IsNullOrEmpty(column.ValueStyle) == false)
                    {
                        style = string.Format("style=\"{0}\"", column.ValueStyle);
                    }
                    html.AppendFormat("<td {0}>", style);

                    var value = column.ValueExpression(data);

                    if (value != null)
                    {
                        html.Append(value);
                    }

                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
        }
    }
}