using Xunit;

namespace Snow.Test.Security.HashHelpers
{
    // ReSharper disable once InconsistentNaming
    public class GetMD5Test : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var hashHelper = BuildHashHelper("优客");

            var md5Bytes = hashHelper.GetMD5();

            var md5Hex = GetHex(md5Bytes);
            Assert.Equal("0E8869D60C581C8A86DB3B7D3992BF11", md5Hex);
        }
    }
}