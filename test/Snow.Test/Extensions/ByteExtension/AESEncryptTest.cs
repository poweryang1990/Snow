﻿using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.ByteExtension
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
            var clearBytes = new byte[] { 1 };
            var key = (byte[])null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => clearBytes.AESEncrypt(key));
        }

        [Fact]
        public void when_key_is_invalid_should_throw_ArgumentException()
        {
            var clearBytes = new byte[] { 1 };
            var key = new byte[] { 1, 2 };

            Assert.Throws<ArgumentException>(() => clearBytes.AESEncrypt(key));
        }

        [Fact]
        public void when_bytes_and_key_is_valid()
        {
            var plaintextBytes = "优客".GetBytes();
            //16byte的key
            var key = "chunqiu".GetBytes().GetMD5();

            //加密
            var encryptedBytes = plaintextBytes.AESEncrypt(key);

            //解密 
            var decryptedBytes = encryptedBytes.AESDecrypt(key);


            Assert.Equal(plaintextBytes, decryptedBytes);
        }
    }
}
