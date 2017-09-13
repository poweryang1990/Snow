using System;
using UokoFramework.Extensions;
using Xunit;

namespace UokoFramework.Test.Extensions.ByteExtension
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
            var encryptedText = "47441BE595AF0A6EBFD1A1864EFE2C83";
            var encryptedBytes = new byte[encryptedText.Length / 2];
            for (var i = 0; i < encryptedBytes.Length; i++)
            {
                encryptedBytes[i] = Convert.ToByte(encryptedText.Substring(i * 2, 2), 16);
            }

            //16byte的key
            var key = "chunqiu".GetBytes().GetMD5();

            //解密
            var clearBytes = encryptedBytes.AESDecrypt(key);

            var clearText = clearBytes.GetString();
            Assert.Equal("优客", clearText);
        }
    }
}
