using System.Security.Cryptography;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Cryptography.AESProviders
{
    public class DecryptTest : BaseTest
    {
        [Theory]
        [InlineData("dT+pG8hcGYWXduM3DeTdXkdEG+WVrwpuv9Ghhk7+LIM=")]
        [InlineData("vA0QShdieXtfnqhzav2mTUdEG+WVrwpuv9Ghhk7+LIM=")]
        public void when_bytes_and_key_is_valid(string encryptedString)
        {
            var encryptedBytes = encryptedString.GetBytesFromBase64();
            var aesProvider = BuildAESProvider("chunqiu");
            aesProvider.Mode = CipherMode.ECB;
            aesProvider.Padding = PaddingMode.Zeros;

            var plaintextBytes = aesProvider.Decrypt(encryptedBytes);

            var plaintext = plaintextBytes.GetString().Replace("\0", "");
            Assert.Equal("优客", plaintext);
        }

        [Fact]
        public void when_bytes_and_key_is_valid_set_mode_and_padding_and_not_use_iv()
        {
            var encryptedBytes = "R0Qb5ZWvCm6/0aGGTv4sgw==".GetBytesFromBase64();
            var aesProvider = BuildAESProvider("chunqiu");
            aesProvider.WithIV = false;
            aesProvider.Mode = CipherMode.ECB;
            aesProvider.Padding = PaddingMode.Zeros;

            //解密
            var plaintextBytes = aesProvider.Decrypt(encryptedBytes);

            var plaintext = plaintextBytes.GetString().Replace("\0", "");
            Assert.Equal("优客", plaintext);
        }
    }
}