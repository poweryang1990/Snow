using Xunit;

// ReSharper disable ExpressionIsAlwaysNull

namespace UOKOFramework.Web.Test.IPHelpers
{
    public class ParseTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("345.123")]
        [InlineData("ab")]
        public void when_ip_is_invalid_should_return_null(string ip)
        {
            var ipHelper = new IPHelper();

            Assert.Null(ipHelper.Parse(ip));
        }

        [Fact]
        public void when_ip_is_valid()
        {
            var ipHelper = new IPHelper();

            Assert.Equal("0.0.0.12", ipHelper.Parse("12").ToString());
            Assert.Equal("1.0.0.2", ipHelper.Parse("1.2").ToString());
            Assert.Equal("1.2.3.4", ipHelper.Parse("1.2.3.4").ToString());
            Assert.Equal("abcd:ef01:2345:6789:abcd:ef01:2345:6789", ipHelper.Parse("ABCD:EF01:2345:6789:ABCD:EF01:2345:6789").ToString());
        }
    }
}
