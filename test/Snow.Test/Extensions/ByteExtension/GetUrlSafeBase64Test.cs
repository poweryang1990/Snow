using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.ByteExtension
{
    public class GetUrlSafeBase64Test
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var bytes = (byte[])null;

            Assert.Throws<ArgumentNullException>(() => bytes.GetUrlSafeBase64());
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var bytes = new byte[] { 62 << 2, 0, 63 };

            var urlSafeBase64 = bytes.GetUrlSafeBase64();

            Assert.Equal("-AA_", urlSafeBase64);
        }
    }
}
