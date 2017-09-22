using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Security.HashProviders
{
    // ReSharper disable once InconsistentNaming
    public class GetHMACMD5Test : BaseTest
    {
        [Fact]
        public void when_key_is_null_should_throw_ArgumentNullException()
        {
            var hashProvider = BuildHashProvider("优客");
            byte[] key = null;

            Assert.Throws<ArgumentNullException>(() => hashProvider.GetHMACMD5(key));
        }

        [Fact]
        public void when_bytes_and_key_is_not_null()
        {
            var hashProvider = BuildHashProvider("优客");
            var key = "chunqiu".GetBytes();

            var macBytes = hashProvider.GetHMACMD5(key);

            var macHex = GetHex(macBytes);
            Assert.Equal("00A3381D9DE44FC7A3617A078D350271", macHex);
        }
    }
}