using Xunit;

namespace UOKOFramework.Test.Security.ASCIIHelpers
{
    public class GetBase64Test : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var asciiHelper = BuildASCIIHelper("优客");

            var base64 = asciiHelper.GetBase64();

            Assert.Equal("5LyY5a6i", base64);
        }
    }
}