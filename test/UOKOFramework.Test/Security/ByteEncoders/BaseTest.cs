using System;
using UOKOFramework.Extensions;
using UOKOFramework.Security;
using Xunit;

namespace UOKOFramework.Test.Security.ByteEncoders
{
    public class BaseTest
    {
        public ByteEncoder BuildByteEncoder(string value)
        {
            var bytes = value.GetBytes();
            return new ByteEncoder(bytes);
        }

        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => new ByteEncoder(bytes));
        }

        [Fact]
        public void when_bytes_is_empty_should_throw_ArgumentNullException()
        {
            byte[] bytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => new ByteEncoder(bytes));
        }
    }
}