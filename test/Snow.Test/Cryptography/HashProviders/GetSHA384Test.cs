using Xunit;

namespace Snow.Test.Cryptography.HashProviders
{
    // ReSharper disable once InconsistentNaming
    public class GetSHA384Test : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var hashHelper = BuildHashProvider("优客");

            var sha484Bytes = hashHelper.GetSHA384();

            var sha384Hex = GetHex(sha484Bytes);
            Assert.Equal("1B98688AB3006FBA5B4D69B0AA5BDD51F9453B8B436F9A53ADA7AE6808C2F0A50BC1219976F4B2FA406636D3179B6F3B", sha384Hex);
        }
    }
}