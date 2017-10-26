using System;
using System.Text;
using Snow.Text;
using Xunit;

namespace Snow.Test.Text.ByteHelpers
{
    public class GetStringTest : BaseTest
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var byteHelper = ByteHelper.New();
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetString(bytes));
        }

        [Fact]
        public void when_bytes_is_empty_should_throw_ArgumentNullException()
        {
            var byteHelper = ByteHelper.New();
            byte[] bytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetString(bytes));
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var byteHelper = ByteHelper.New();

            var value = byteHelper.GetString(new byte[] { 65, 66, 67 }, Encoding.ASCII);

            Assert.Equal("ABC", value);
        }
    }
}