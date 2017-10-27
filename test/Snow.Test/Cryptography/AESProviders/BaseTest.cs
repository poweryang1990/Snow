using System;
using Snow.Cryptography;
using Snow.Extensions;
using Xunit;

// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable InconsistentNaming

namespace Snow.Test.Cryptography.AESProviders
{
    public class BaseTest
    {
        public AESProvider BuildAESProvider(string key)
        {
            var keyBytes = key.GetBytes().GetMD5();
            return AESProvider.New(keyBytes);
        }

        [Fact]
        public void when_key_is_null_should_throw_ArgumentNullException()
        {
            byte[] keyBytes = null;

            Assert.Throws<ArgumentNullException>(() => AESProvider.New(keyBytes));
        }

        [Fact]
        public void when_key_is_empty_should_throw_ArgumentNullException()
        {
            byte[] keyBytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => AESProvider.New(keyBytes));
        }

        [Fact]
        public void when_key_is_invalid_should_throw_ArgumentException()
        {
            var keyBytes = new byte[] { 1, 2 };

            Assert.Throws<ArgumentException>(() => AESProvider.New(keyBytes));
        }
    }
}