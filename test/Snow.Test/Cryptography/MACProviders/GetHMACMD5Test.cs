using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Cryptography.MACProviders
{
    // ReSharper disable once InconsistentNaming
    public class GetHMACMD5Test : BaseTest
    {
        [Fact]
        public void when_key_is_null_should_throw_ArgumentNullException()
        {
            var macProvider = BuildMACProvider("优客");
            byte[] key = null;

            Assert.Throws<ArgumentNullException>(() => macProvider.GetHMACMD5(key));
        }

        [Fact]
        public void when_bytes_and_key_is_not_null()
        {
            var macProvider = BuildMACProvider("优客");
            var key = "chunqiu".GetBytes();

            var macBytes = macProvider.GetHMACMD5(key);

            var macHex = GetHex(macBytes);
            Assert.Equal("00A3381D9DE44FC7A3617A078D350271", macHex);
        }
    }
}