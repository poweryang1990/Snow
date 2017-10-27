using Snow.Extensions;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Snow.Test.Cryptography.AESHelpers
{
    public class EncryptTest : BaseTest
    {
        [Fact]
        public void when_bytes_and_key_is_valid()
        {
            var plaintextBytes = "优客".GetBytes();
            var aesHelper = BuildAESHelper("chunqiu");

            //加密
            var encryptedBytes = aesHelper.Encrypt(plaintextBytes);

            var encryptedTextOfBase64 = encryptedBytes.GetBase64();
            Assert.Equal("R0Qb5ZWvCm6/0aGGTv4sgw==", encryptedTextOfBase64);
        }
    }
}