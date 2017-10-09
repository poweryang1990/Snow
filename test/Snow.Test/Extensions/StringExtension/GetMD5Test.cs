using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.StringExtension
{
    public class GetMD5Test
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            string value = null;

            Assert.Throws<ArgumentNullException>(() => value.GetMD5());
        }

        [Fact]
        public void when_value_is_not_null()
        {
            Assert.Equal("0E8869D60C581C8A86DB3B7D3992BF11", "优客".GetMD5());
        }
    }
}
