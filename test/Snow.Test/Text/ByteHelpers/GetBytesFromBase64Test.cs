using System;
using Snow.Text;
using Xunit;

namespace Snow.Test.Text.ByteHelpers
{
    public class GetBytesFromBase64Test : BaseTest
    {
        [Fact]
        public void when_string_is_invalid_should_throw_ArgumentNullException()
        {
            var byteHelper = new ByteHelper();

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBytesFromBase64(null));
            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBytesFromBase64(""));
            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBytesFromBase64(" "));
            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBytesFromBase64("\r\t"));
        }

        [Fact]
        public void when_base64_is_not_null()
        {
            var byteHelper = new ByteHelper();

            var bytes = byteHelper.GetBytesFromBase64("5LyY5a6i");

            Assert.Equal(new byte[] { 228, 188, 152, 229, 174, 162 }, bytes);
        }

        [Fact]
        public void when_base64_is_url_safe()
        {
            var byteHelper = new ByteHelper();

            var bytes = byteHelper.GetBytesFromBase64("-AA_");

            Assert.Equal(new byte[] { 62 << 2, 0, 63 }, bytes);
        }
    }
}