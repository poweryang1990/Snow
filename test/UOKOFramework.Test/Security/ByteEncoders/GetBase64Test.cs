using Xunit;

namespace UOKOFramework.Test.Security.ByteEncoders
{
    public class GetBase64Test : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var byteEncoder = BuildByteEncoder("优客");

            var base64 = byteEncoder.GetBase64();

            Assert.Equal("5LyY5a6i", base64);
        }
    }
}