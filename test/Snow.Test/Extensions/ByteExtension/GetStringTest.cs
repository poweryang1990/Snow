using System;
using System.Text;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.ByteExtension
{
    public class GetStringTest
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var bytes = (byte[])null;

            Assert.Throws<ArgumentNullException>(() => bytes.GetString());
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var bytes = new byte[] { 65, 66, 67 };

            var value = bytes.GetString(Encoding.ASCII);

            Assert.Equal("ABC", value);
        }
    }
}
