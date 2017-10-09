using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.DateTimeExtension
{
    public class ToUnixTimeSecondsTest
    {
        [Fact]
        public void ToUnixTimeSeconds()
        {
            var dateTime1 = new DateTime(1970, 1, 1, 0, 0, 1, DateTimeKind.Utc);
            var dateTime2 = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Utc);

            var unixtime1 = dateTime1.ToUnixTimeSeconds();
            var unixtime2 = dateTime2.ToUnixTimeSeconds();

            Assert.Equal(1, unixtime1);
            Assert.Equal(60 * 60 * 8, unixtime2);
        }
    }
}
