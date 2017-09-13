using System;
using UokoFramework.Extensions;
using Xunit;

namespace UokoFramework.Test.Extensions.ByteExtension
{
    public class AESEncryptTest
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var clearByte = (byte[])null;
            var key = Guid.NewGuid().ToByteArray();

            Assert.Throws<ArgumentNullException>(() => clearByte.AESEncrypt(key));
        }

        [Fact]
        public void when_key_is_null_should_throw_ArgumentNullException()
        {
            var clearBytes = new byte []{1};
            var key = (byte[])null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => clearBytes.AESEncrypt(key));
        }

        [Fact]
        public void when_key_is_invalid_should_throw_ArgumentException()
        {
            var clearBytes = new byte[] { 1 };
            var key = new byte[] { 1,2 };

            Assert.Throws<ArgumentException>(() => clearBytes.AESEncrypt(key));
        }

        [Fact]
        public void when_bytes_and_key_is_valid()
        {
            var clearBytes = "优客".GetBytes();
            //16byte的key
            var key = "chunqiu".GetBytes().GetMD5();

            //加密
            var encryptedBytes = clearBytes.AESEncrypt(key);

            var encryptedHex = encryptedBytes.GetHex(false, false);
            Assert.Equal("47441BE595AF0A6EBFD1A1864EFE2C83", encryptedHex);
        }
    }
}
