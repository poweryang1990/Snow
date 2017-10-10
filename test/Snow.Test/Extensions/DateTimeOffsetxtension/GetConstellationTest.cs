using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.DateTimeOffsetxtension
{
    public class GetConstellationTest
    {
        [Theory]
        [InlineData("2016-12-22", "2017-01-19", "摩羯座", Constellation.Capricorn)]
        [InlineData("2017-01-20", "2017-02-18", "水瓶座", Constellation.Aquarius)]
        [InlineData("2017-02-19", "2017-03-20", "双鱼座", Constellation.Pisces)]
        [InlineData("2017-03-21", "2017-04-19", "白羊座", Constellation.Aries)]
        [InlineData("2017-04-20", "2017-05-20", "金牛座", Constellation.Taurus)]
        [InlineData("2017-05-21", "2017-06-21", "双子座", Constellation.Gemini)]
        [InlineData("2017-06-22", "2017-07-22", "巨蟹座", Constellation.Cancer)]
        [InlineData("2017-07-23", "2017-08-22", "狮子座", Constellation.Leo)]
        [InlineData("2017-08-23", "2017-09-22", "处女座", Constellation.Virgo)]
        [InlineData("2017-09-23", "2017-10-23", "天秤座", Constellation.Libra)]
        [InlineData("2017-10-24", "2017-11-21", "天蝎座", Constellation.Scorpio)]
        [InlineData("2017-11-22", "2017-12-21", "射手座", Constellation.Sagittarius)]
        public void GetConstellation(string begin, string end, string constellationString, Constellation constellation)
        {
            var beginDate = DateTimeOffset.Parse(begin);
            var endDate = DateTimeOffset.Parse(end);
            do
            {
                Assert.Equal(constellationString, beginDate.GetConstellationString());
                Assert.Equal(constellation, beginDate.GetConstellation());
                beginDate = beginDate.AddDays(1);
            } while (beginDate <= endDate);
        }
    }
}
