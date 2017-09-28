using Xunit;
// ReSharper disable InconsistentNaming
// ReSharper disable ExpressionIsAlwaysNull

namespace UOKOFramework.Test.IPHelpers
{
    public class ToIPv4Test
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("345.123")]
        [InlineData("ab")]
        public void when_ip_is_invalid_should_return_null(string ip)
        {
            var ipHelper = new IPHelper();

            Assert.Null(ipHelper.ToIPv4(ip));
        }

        [Fact]
        public void when_ip_is_valid()
        {
            var ipHelper = new IPHelper();

            Assert.Equal("0.0.0.12", ipHelper.ToIPv4("12").ToString());
            Assert.Equal("1.0.0.2", ipHelper.ToIPv4("1.2").ToString());
            Assert.Equal("1.2.3.4", ipHelper.ToIPv4("1.2.3.4").ToString());
            Assert.Equal(null, ipHelper.ToIPv4("ABCD:EF01:2345:6789:ABCD:EF01:2345:6789"));
        }
    }
}
