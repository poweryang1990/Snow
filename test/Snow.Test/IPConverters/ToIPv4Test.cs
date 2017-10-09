using Xunit;
// ReSharper disable InconsistentNaming
// ReSharper disable ExpressionIsAlwaysNull

namespace Snow.Test.IPConverters
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
            Assert.Null(IPConverter.ToIPv4(ip));
        }

        [Fact]
        public void when_ip_is_valid()
        {
            Assert.Equal("0.0.0.12", IPConverter.ToIPv4("12").ToString());
            Assert.Equal("1.0.0.2", IPConverter.ToIPv4("1.2").ToString());
            Assert.Equal("1.2.3.4", IPConverter.ToIPv4("1.2.3.4").ToString());
            Assert.Equal(null, IPConverter.ToIPv4("ABCD:EF01:2345:6789:ABCD:EF01:2345:6789"));
        }
    }
}
