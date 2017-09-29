using Xunit;
// ReSharper disable PossibleInvalidOperationException
// ReSharper disable InconsistentNaming

namespace UOKOFramework.IPDB.Test
{
    public class IPLocatorTest
    {
        [Fact]
        public void when_ip_is_valid()
        {
            var ipLocator = IPLocator.Default;

            var ipv4 = IPConverter.ToIPv4("113.208.112.114").Value;

            var location = ipLocator.Find(ipv4);

            Assert.Equal("中国", location.Country);
            Assert.Equal("北京", location.State);
            Assert.Equal("北京", location.City);
            Assert.Equal("113.208.112.114", location.IP.ToString());
        }
    }
}