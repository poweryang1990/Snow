using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.ByteExtension
{
    public class GetSHA256Test
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var bytes = (byte[])null;

            Assert.Throws<ArgumentNullException>(() => bytes.GetSHA256());
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var bytes = "优客".GetBytes();

            var sha256 = bytes.GetSHA256();

            var sha256Hex = sha256.GetHex(withHyphen: false, lowerCase: false);
            Assert.Equal("44E77E370BD3FAFA99DD21E86BD7D7E9407D146F12EE3DD36AFF248B9E012482", sha256Hex);
        }
    }
}
