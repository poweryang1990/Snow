using System;
using Snow.Text;
using Xunit;

namespace Snow.Test.Text.ByteHelpers
{
    public class GetHexTest : BaseTest
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var byteHelper = new ByteHelper();
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetHex(bytes));
        }

        [Fact]
        public void when_bytes_is_empty_should_throw_ArgumentNullException()
        {
            var byteHelper = new ByteHelper();
            byte[] bytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetHex(bytes));
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var byteHelper = new ByteHelper();
            var bytes = byteHelper.GetBytes("优客");

            var hex = byteHelper.GetHex(bytes);

            Assert.Equal("E4-BC-98-E5-AE-A2", hex);
        }

        [Fact]
        public void when_withHyphen_is_true()
        {
            var byteHelper = new ByteHelper();
            var bytes = byteHelper.GetBytes("优客");

            var hex = byteHelper.GetHex(bytes, withHyphen: true);

            Assert.Equal("E4-BC-98-E5-AE-A2", hex);
        }

        [Fact]
        public void when_withHyphen_is_false()
        {
            var byteHelper = new ByteHelper();
            var bytes = byteHelper.GetBytes("优客");

            var hex = byteHelper.GetHex(bytes, withHyphen: false);

            Assert.Equal("E4BC98E5AEA2", hex);
        }

        [Fact]
        public void when_lowerCase_is_true()
        {
            var byteHelper = new ByteHelper();
            var bytes = byteHelper.GetBytes("优客");

            var hex = byteHelper.GetHex(bytes, withHyphen: true, lowerCase: true);

            Assert.Equal("e4-bc-98-e5-ae-a2", hex);
        }
    }
}