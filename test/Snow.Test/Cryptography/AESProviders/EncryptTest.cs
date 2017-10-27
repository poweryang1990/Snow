using System.Security.Cryptography;
using Snow.Extensions;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Snow.Test.Cryptography.AESProviders
{
    public class EncryptTest : BaseTest
    {
        [Fact]
        public void when_bytes_and_key_is_valid()
        {
            var plaintextBytes = "优客".GetBytes();
            var aesProvider = BuildAESProvider("chunqiu");

            //加密
            var encryptedBytes = aesProvider.Encrypt(plaintextBytes);
            //解密
            var decryptedBytes = aesProvider.Decrypt(encryptedBytes);

            //由于IV每次运行会随机变化，则加密结果也会随机变化，则这里只通过解密来反向验证。
            Assert.Equal(plaintextBytes, decryptedBytes);
        }

        [Fact]
        public void when_bytes_and_key_is_valid_set_mode_and_padding_and_not_use_iv()
        {
            var plaintextBytes = "优客".GetBytes();
            var aesProvider = BuildAESProvider("chunqiu");
            aesProvider.WithIV = false;
            aesProvider.Mode = CipherMode.ECB;
            aesProvider.Padding = PaddingMode.Zeros;

            //加密
            var encryptedBytes = aesProvider.Encrypt(plaintextBytes);

            var encryptedTextOfBase64 = encryptedBytes.GetBase64();
            Assert.Equal("R0Qb5ZWvCm6/0aGGTv4sgw==", encryptedTextOfBase64);
        }
    }
}