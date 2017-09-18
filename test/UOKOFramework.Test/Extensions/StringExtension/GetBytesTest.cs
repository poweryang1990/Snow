using System;
using System.Text;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.StringExtension
{
    public class GetBytesTest
    {
        [Fact]
        public void when_string_is_null_should_throw_ArgumentNullException()
        {
            string value = null;

            Assert.Throws<ArgumentNullException>(() => value.GetBytes());
        }

        [Fact]
        public void when_string_is_not_null()
        {
            var value = "优客";

            var bytes = value.GetBytes();

            Assert.Equal(new byte[] { 228, 188, 152, 229, 174, 162 }, bytes);
        }

        [Fact]
        public void when_string_is_not_null_and_encoding_is_utf8()
        {
            var value = "优客";
            var utf8Bytes = value.GetBytes(Encoding.UTF8);

            var bytes = value.GetBytes();

            Assert.Equal(bytes, utf8Bytes);
        }

        [Fact]
        public void when_string_is_not_null_and_encoding_is_gb2312()
        {
            var value = "优客";

            var bytes = value.GetBytes(Encoding.GetEncoding("GB2312"));

            Assert.Equal(new byte[] { 211, 197, 191, 205 }, bytes);
        }
    }
}
