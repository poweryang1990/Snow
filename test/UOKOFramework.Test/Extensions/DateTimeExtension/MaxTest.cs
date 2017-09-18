using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.DateTimeExtension
{
    public class MaxTest
    {
        [Fact]
        public void Max()
        {
            var dateTimeMin = DateTime.Parse("2017-1-1");
            var dateTimeMax = DateTime.Parse("2017-1-2");

            Assert.Equal(dateTimeMin, dateTimeMin.Max(dateTimeMin));
            Assert.Equal(dateTimeMax, dateTimeMin.Max(dateTimeMax));
            Assert.Equal(dateTimeMax, dateTimeMax.Max(dateTimeMin));
        }
    }
}
