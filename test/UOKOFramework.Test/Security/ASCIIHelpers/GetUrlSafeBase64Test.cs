using UOKOFramework.Security;
using Xunit;

namespace UOKOFramework.Test.Security.ASCIIHelpers
{
    public class GetUrlSafeBase64Test : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var asciiHelper = new ASCIIHelper(
                new byte[] { 62 << 2, 0, 63 }
            );

            var urlSafeBase64 = asciiHelper.GetUrlSafeBase64();

            Assert.Equal("-AA_", urlSafeBase64);
        }
    }
}