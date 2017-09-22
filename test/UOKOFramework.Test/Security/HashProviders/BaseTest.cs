using System;
using UOKOFramework.Extensions;
using UOKOFramework.Security;
using Xunit;

namespace UOKOFramework.Test.Security.HashProviders
{
    public class BaseTest
    {
        public HashProvider BuildHashProvider(string value)
        {
            var bytes = value.GetBytes();
            return new HashProvider(bytes);
        }

        public string GetHex(byte[] bytes)
        {
            var byteEncoder = new ByteEncoder(bytes);

            return byteEncoder.GetHex(withHyphen: false, lowerCase: false);
        }

        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => new HashProvider(bytes));
        }

        [Fact]
        public void when_bytes_is_empty_should_throw_ArgumentNullException()
        {
            byte[] bytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => new HashProvider(bytes));
        }
    }
}