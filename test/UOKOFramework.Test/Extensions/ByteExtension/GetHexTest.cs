using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.ByteExtension
{
    public class GetHexTest
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var bytes = (byte[])null;

            Assert.Throws<ArgumentNullException>(() => bytes.GetHex());
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var bytes = "优客".GetBytes();

            var hex = bytes.GetHex();

            Assert.Equal("E4-BC-98-E5-AE-A2", hex);
        }

        [Fact]
        public void when_withHyphen_is_true()
        {
            var bytes = "优客".GetBytes();

            var hex = bytes.GetHex(withHyphen: true);

            Assert.Equal("E4-BC-98-E5-AE-A2", hex);
        }

        [Fact]
        public void when_withHyphen_is_false()
        {
            var bytes = "优客".GetBytes();

            var hex = bytes.GetHex(withHyphen: false);

            Assert.Equal("E4BC98E5AEA2", hex);
        }

        [Fact]
        public void when_lowerCase_is_true()
        {
            var bytes = "优客".GetBytes();

            var hex = bytes.GetHex(withHyphen: true, lowerCase: true);

            Assert.Equal("e4-bc-98-e5-ae-a2", hex);
        }
    }
}
