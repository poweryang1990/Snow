using System;
using System.Text;
using UokoFramework.Extensions;
using Xunit;

namespace UokoFramework.Test.Extensions.StringExtension
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
        public void when_string_is_valid()
        {
            var base64 = "5LyY5a6i";
            var bytes = "优客".GetBytes();

            var bytesFromBase64 = base64.GetBytesFromBase64();

            Assert.Equal(bytes, bytesFromBase64);
        }
    }
}
