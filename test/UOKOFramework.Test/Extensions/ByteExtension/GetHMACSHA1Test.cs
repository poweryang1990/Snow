using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.ByteExtension
{
    // ReSharper disable once InconsistentNaming
    public class GetHMACSHA1Test
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var bytes = (byte[])null;
            var key = new byte[] { 1 };

            Assert.Throws<ArgumentNullException>(() => bytes.GetHMACSHA1(key));
        }

        [Fact]
        public void when_key_is_null_should_throw_ArgumentNullException()
        {
            var bytes = new byte[] { 1 };
            var key = (byte[])null;

            Assert.Throws<ArgumentNullException>(() => bytes.GetHMACSHA1(key));
        }

        [Fact]
        public void when_bytes_and_key_is_not_null()
        {
            var bytes = "优客".GetBytes();
            var key = "chunqiu".GetBytes();

            var mac = bytes.GetHMACSHA1(key);

            var macOfBase64 = mac.GetBase64();
            Assert.Equal("mE6gOLautI4MoGJKdBCakUahqMk=", macOfBase64);
        }
    }
}
