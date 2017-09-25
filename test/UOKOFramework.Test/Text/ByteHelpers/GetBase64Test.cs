using System;
using UOKOFramework.Text;
using Xunit;

namespace UOKOFramework.Test.Text.ByteHelpers
{
    public class GetBase64Test : BaseTest
    {

        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var byteHelper = new ByteHelper();
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBase64(bytes));
        }

        [Fact]
        public void when_bytes_is_empty_should_throw_ArgumentNullException()
        {
            var byteHelper = new ByteHelper();
            byte[] bytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBase64(bytes));
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var byteHelper = new ByteHelper();
            var bytes = byteHelper.GetBytes("优客");

            var base64 = byteHelper.GetBase64(bytes);

            Assert.Equal("5LyY5a6i", base64);
        }
    }
}