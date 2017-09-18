using System;
using System.Text;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.StringExtension
{
    public class GetBytesFromBase64Test
    {
        [Fact]
        public void when_string_is_null_should_throw_ArgumentNullException()
        {
            string value = null;

            Assert.Throws<ArgumentNullException>(() => value.GetBytesFromBase64());
        }

        [Fact]
        public void when_string_is_invalid_base64()
        {
            var value = "优客";

            Assert.Throws<FormatException>(() => value.GetBytesFromBase64());
        }

        [Fact]
        public void when_string_is_base64()
        {
            var base64 = "5LyY5a6i";
            var bytes = "优客".GetBytes();

            var bytesFromBase64 = base64.GetBytesFromBase64();

            Assert.Equal(bytes, bytesFromBase64);
        }

        [Fact]
        public void when_string_is_url_safe_base64()
        {
            var urlSafeBase64 = "-AA_";

            var bytesFromBase64 = urlSafeBase64.GetBytesFromBase64();

            Assert.Equal(new byte[] { 62 << 2, 0, 63 }, bytesFromBase64);
        }
    }
}
