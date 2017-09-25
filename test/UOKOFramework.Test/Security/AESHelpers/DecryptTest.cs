﻿using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Security.AESHelpers
{
    public class DecryptTest : BaseTest
    {
        [Fact]
        public void when_bytes_and_key_is_valid()
        {
            var encryptedBytes = "R0Qb5ZWvCm6/0aGGTv4sgw==".GetBytesFromBase64();
            var aesHelper = BuildAESHelper("chunqiu");

            //解密
            var plaintextBytes = aesHelper.Decrypt(encryptedBytes);

            var plaintext = plaintextBytes.GetString();
            Assert.Equal("优客", plaintext);
        }
    }
}