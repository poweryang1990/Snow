using System;
using UokoFramework.Extensions;
using Xunit;

namespace UokoFramework.Test.Extensions.ByteExtension
{
    public class GetMD5Test
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var bytes = (byte[])null;

            Assert.Throws<ArgumentNullException>(() => bytes.GetMD5());
        }
    }
}
