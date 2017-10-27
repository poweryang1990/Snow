using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Cryptography.MACProviders
{
    // ReSharper disable once InconsistentNaming
    public class GetHMACSHA256Test : BaseTest
    {
        [Fact]
        public void when_key_is_null_should_throw_ArgumentNullException()
        {
            var macProvider = BuildMACProvider("优客");
            byte[] key = null;

            Assert.Throws<ArgumentNullException>(() => macProvider.GetHMACSHA256(key));
        }

        [Fact]
        public void when_bytes_and_key_is_not_null()
        {
            var macProvider = BuildMACProvider("优客");
            var key = "chunqiu".GetBytes();

            var macBytes = macProvider.GetHMACSHA256(key);

            var macHex = GetHex(macBytes);
            Assert.Equal("F0B069408A3E61B6B417064CB774C5522CB2DCD2E971CC863B2C322967B4BBCE", macHex);
        }
    }
}