using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Cryptography.HashHelpers
{
    // ReSharper disable once InconsistentNaming
    public class GetHMACSHA1Test : BaseTest
    {
        [Fact]
        public void when_key_is_null_should_throw_ArgumentNullException()
        {
            var hashHelper = BuildHashHelper("优客");
            byte[] key = null;

            Assert.Throws<ArgumentNullException>(() => hashHelper.GetHMACSHA1(key));
        }

        [Fact]
        public void when_bytes_and_key_is_not_null()
        {
            var hashHelper = BuildHashHelper("优客");
            var key = "chunqiu".GetBytes();

            var macBytes = hashHelper.GetHMACSHA1(key);

            var macHex = GetHex(macBytes);
            Assert.Equal("984EA038B6AEB48E0CA0624A74109A9146A1A8C9", macHex);
            Assert.Equal("984EA038B6AEB48E0CA0624A74109A9146A1A8C9", macHex);
        }
    }
}