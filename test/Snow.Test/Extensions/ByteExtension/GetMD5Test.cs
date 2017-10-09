using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.ByteExtension
{
    public class GetMD5Test
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var bytes = (byte[])null;

            Assert.Throws<ArgumentNullException>(() => bytes.GetMD5());
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var bytes = "优客".GetBytes();

            var md5 = bytes.GetMD5();

            var md5Hex = md5.GetHex(withHyphen: false, lowerCase: false);
            Assert.Equal("0E8869D60C581C8A86DB3B7D3992BF11", md5Hex);
        }
    }
}
