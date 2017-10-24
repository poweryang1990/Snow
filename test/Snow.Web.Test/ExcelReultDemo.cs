using System.Collections.Generic;
using System.Web.Mvc;
using Snow.Extensions;
using Xunit;

namespace Snow.Web.Test
{
    public class ExcelReultDemo
    {
        public class Data
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public void Demo()
        {
            var items = new List<Data>
            {
                new Data{ Name="chunqiu", Age=12},
                new Data{ Name="chunqiu2", Age=13}
            };

            ActionResult excelResult = items
                  .NewExcelResultBuilder("用户列表")
                  .AddTextColumn("姓名", _ => _.Name)
                  .AddColumn("年龄", _ => _.Age)
                  .Build("用户列表 2017-10-24.xls");

        }
    }
}
