using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.DateTimeExtension
{
    public class GetUnixtimeTest
    {
        [Fact]
        public void GetUnixtime()
        {
            var dateTime1 = new DateTime(1970, 1, 1, 0, 0, 1, DateTimeKind.Utc);
            var dateTime2 = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Utc);

            var unixtime1 = dateTime1.GetUnixtime();
            var unixtime2 = dateTime2.GetUnixtime();

            Assert.Equal(1, unixtime1);
            Assert.Equal(60 * 60 * 8, unixtime2);
        }
    }
}
