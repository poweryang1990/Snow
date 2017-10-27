using System;
using Snow.Text;
using Xunit;
// ReSharper disable ExpressionIsAlwaysNull

namespace Snow.Test.Text.ByteHelpers
{
    public class GetBytesFromHexTest : BaseTest
    {
        [Fact]
        public void when_hex_is_null_should_throw_ArgumentNullException()
        {
            var byteHelper = ByteHelper.New();
            string hex = null;

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBytesFromHex(hex));
        }

        [Fact]
        public void when_hex_is_invalid_hex_should_throw_ArgumentException()
        {
            var byteHelper = ByteHelper.New();
            var hex = "asd";

            Assert.Throws<ArgumentException>(() => byteHelper.GetBytesFromHex(hex));
        }

        [Fact]
        public void when_hex_is_valid()
        {
            var byteHelper = ByteHelper.New();
            var bytes = byteHelper.GetBytes("优客");

            var hex1 = byteHelper.GetBytesFromHex("e4bc98e5aea2");
            var hex2 = byteHelper.GetBytesFromHex("E4BC98E5AEA2");
            var hex3 = byteHelper.GetBytesFromHex("e4-bc-98-e5-ae-a2");
            var hex4 = byteHelper.GetBytesFromHex("E4-BC-98-E5-AE-A2");

            Assert.Equal(bytes, hex1);
            Assert.Equal(bytes, hex2);
            Assert.Equal(bytes, hex3);
            Assert.Equal(bytes, hex4);
        }
    }
}