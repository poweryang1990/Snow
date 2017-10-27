using System;
using Snow.Extensions;
using Snow.Test.Text.ByteHelpers;
using Snow.Text;
using Xunit;

// ReSharper disable ExpressionIsAlwaysNull

namespace Snow.Test.Extensions.StringExtension
{
    public class GetBytesFromHexTest : BaseTest
    {
        [Fact]
        public void when_hex_is_null_should_throw_ArgumentNullException()
        {
            string hex = null;

            Assert.Throws<ArgumentNullException>(() => hex.GetBytesFromHex());
        }

        [Fact]
        public void when_hex_is_invalid_hex_should_throw_ArgumentException()
        {
            var hex = "asd";

            Assert.Throws<ArgumentException>(() => hex.GetBytesFromHex());
        }

        [Fact]
        public void when_hex_is_valid()
        {
            var bytes = "优客".GetBytes();

            var hex1 = "e4bc98e5aea2".GetBytesFromHex();
            var hex2 = "E4BC98E5AEA2".GetBytesFromHex();
            var hex3 = "e4-bc-98-e5-ae-a2".GetBytesFromHex();
            var hex4 = "E4-BC-98-E5-AE-A2".GetBytesFromHex();

            Assert.Equal(bytes, hex1);
            Assert.Equal(bytes, hex2);
            Assert.Equal(bytes, hex3);
            Assert.Equal(bytes, hex4);
        }
    }
}