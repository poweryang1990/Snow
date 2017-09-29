using Xunit;

// ReSharper disable InconsistentNaming
// ReSharper disable ExpressionIsAlwaysNull

namespace UOKOFramework.Test.IPConverters
{
    public class ToIPAddressTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("345.123")]
        [InlineData("ab")]
        public void when_ip_is_invalid_should_return_null(string ip)
        {
            Assert.Null(IPConverter.ToIPAddress(ip));
        }

        [Fact]
        public void when_ip_is_valid()
        {
            Assert.Equal("0.0.0.12", IPConverter.ToIPAddress("12").ToString());
            Assert.Equal("1.0.0.2", IPConverter.ToIPAddress("1.2").ToString());
            Assert.Equal("1.2.3.4", IPConverter.ToIPAddress("1.2.3.4").ToString());
            Assert.Equal("abcd:ef01:2345:6789:abcd:ef01:2345:6789", IPConverter.ToIPAddress("ABCD:EF01:2345:6789:ABCD:EF01:2345:6789").ToString());
        }
    }
}
