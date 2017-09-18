using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.ByteExtension
{
    public class GetSHA1Test
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var bytes = (byte[])null;

            Assert.Throws<ArgumentNullException>(() => bytes.GetSHA1());
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var bytes = "优客".GetBytes();

            var sha1 = bytes.GetSHA1();

            var sha1Hex = sha1.GetHex(withHyphen: false, lowerCase: false);
            Assert.Equal("E18A69ABFAB46709CA24105B92A1E425BDD75348", sha1Hex);
        }
    }
}
