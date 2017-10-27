using Xunit;

namespace Snow.Test.Cryptography.HashProviders
{
    // ReSharper disable once InconsistentNaming
    public class GetSHA512Test : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var hashHelper = BuildHashProvider("优客");

            var sha512Bytes = hashHelper.GetSHA512();

            var sha512Hex = GetHex(sha512Bytes);
            Assert.Equal("56CC685EC3825AB47487FEEF57544E24F974188EB8D1D486B51896B1C295D3912A5B54ECEC8981868930D136888843609C9A6F7899BCA7AD4B7681B86D39F185", sha512Hex);
        }
    }
}