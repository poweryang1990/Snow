using System;
using Snow.Text;
using Xunit;

namespace Snow.Test.Text.ByteHelpers
{
    public class GetUrlSafeBase64Test : BaseTest
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var byteHelper = ByteHelper.New();
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetUrlSafeBase64(bytes));
        }

        [Fact]
        public void when_bytes_is_empty_should_throw_ArgumentNullException()
        {
            var byteHelper = ByteHelper.New();
            byte[] bytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetUrlSafeBase64(bytes));
        }


        [Fact]
        public void when_bytes_is_not_null()
        {
            var byteHelper = ByteHelper.New();

            var urlSafeBase64 = byteHelper
                .GetUrlSafeBase64(new byte[] { 62 << 2, 0, 63 });

            Assert.Equal("-AA_", urlSafeBase64);
        }
    }
}