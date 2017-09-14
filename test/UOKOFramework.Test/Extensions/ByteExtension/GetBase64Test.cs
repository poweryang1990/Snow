using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.ByteExtension
{
    public class GetBase64Test
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var bytes = (byte[])null;

            Assert.Throws<ArgumentNullException>(() => bytes.GetBase64());
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var bytes = "优客".GetBytes();

            var base64 = bytes.GetBase64();

            Assert.Equal("5LyY5a6i", base64);
        }
    }
}
