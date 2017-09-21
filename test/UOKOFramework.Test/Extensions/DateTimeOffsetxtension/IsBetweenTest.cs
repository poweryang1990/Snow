using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.DateTimeOffsetxtension
{
    public class IsBetweenTest
    {
        [Theory]
        [InlineData("2012-12-12 12:12:12", "2015-12-12 12:12:12", "2016-12-12 12:12:12", true)]
        [InlineData("2012-12-12 12:12:12", "2012-12-12 12:12:12", "2016-12-12 12:12:12", false)]
        [InlineData("2012-12-12 12:12:12", "2016-12-12 12:12:12", "2016-12-12 12:12:12", false)]
        [InlineData("2012-12-12 12:12:12", "2011-12-12 12:12:12", "2016-12-12 12:12:12", false)]
        [InlineData("2012-12-12 12:12:12", "2017-12-12 12:12:12", "2016-12-12 12:12:12", false)]
        public void IsBetween(string begin, string current, string end, bool isBetween)
        {
            var beginTime = DateTimeOffset.Parse(begin);
            var currentTime = DateTimeOffset.Parse(current);
            var endTime = DateTimeOffset.Parse(end);

            Assert.Equal(isBetween, currentTime.IsBetween(beginTime, endTime));
        }
    }
}
