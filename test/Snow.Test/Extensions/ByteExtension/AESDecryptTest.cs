using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.ByteExtension
{
    public class AESDecryptTest
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var clearByte = (byte[])null;
            var key = Guid.NewGuid().ToByteArray();

            Assert.Throws<ArgumentNullException>(() => clearByte.AESDecrypt(key));
        }

        [Fact]
        public void when_key_is_null_should_throw_ArgumentNullException()
        {
            var clearByte = new byte []{1};
            var key = (byte[])null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => clearByte.AESDecrypt(key));
        }

        [Fact]
        public void when_key_is_invalid_should_throw_ArgumentException()
        {
            var clearByte = new byte[] { 1 };
            var key = new byte[] { 1,2 };

            Assert.Throws<ArgumentException>(() => clearByte.AESEncrypt(key));
        }

        [Fact]
        public void when_bytes_and_key_is_valid()
        {
            var encryptedBytes = "R0Qb5ZWvCm6/0aGGTv4sgw==".GetBytesFromBase64();
            //16byte的key
            var key = "chunqiu".GetBytes().GetMD5();

            //解密
            var plaintextBytes = encryptedBytes.AESDecrypt(key);

            var plaintext = plaintextBytes.GetString();
            Assert.Equal("优客", plaintext);
        }
    }
}
