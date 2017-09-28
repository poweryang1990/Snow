using System;
using Xunit;
// ReSharper disable InconsistentNaming

namespace UOKOFramework.IPDB.Test
{
    public class IPv4DBTest
    {
        [Fact]
        public void Find()
        {
            var ipResult = IPv4DB.Find("113.208.112.114");
            Assert.Equal("中国", ipResult[0]);
            Assert.Equal("北京", ipResult[1]);
            Assert.Equal("北京", ipResult[2]);
        }
    }
}
