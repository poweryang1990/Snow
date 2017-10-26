using System;
using System.Text;
using Snow.Text;
using Xunit;

namespace Snow.Test.Text.ByteHelpers
{
    public class GetBytesTest : BaseTest
    {
        [Fact]
        public void when_string_is_invalid_should_throw_ArgumentNullException()
        {
            var byteHelper = ByteHelper.New();

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBytes(null));
            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBytes(""));
            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBytes(" "));
            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBytes("\r\t"));
        }

        [Fact]
        public void when_string_is_not_null()
        {
            var byteHelper = ByteHelper.New();

            var bytes = byteHelper.GetBytes("优客");

            Assert.Equal(new byte[] { 228, 188, 152, 229, 174, 162 }, bytes);
        }

        [Fact]
        public void when_encoding_is_not_null()
        {
            var byteHelper = ByteHelper.New();

            var bytes = byteHelper.GetBytes("优客", Encoding.GetEncoding("gb2312"));

            Assert.Equal(new byte[] { 211, 197, 191, 205 }, bytes);
        }
    }
}