using System;
using UOKOFramework.Extensions;
using UOKOFramework.Security;
using Xunit;

// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable InconsistentNaming

namespace UOKOFramework.Test.Security.AESHelpers
{
    public class BaseTest
    {
        public AESHelper BuildAESHelper(string key)
        {
            var keyBytes = key.GetBytes().GetMD5();
            return new AESHelper(keyBytes);
        }

        [Fact]
        public void when_key_is_null_should_throw_ArgumentNullException()
        {
            byte[] keyBytes = null;

            Assert.Throws<ArgumentNullException>(() => new AESHelper(keyBytes));
        }

        [Fact]
        public void when_key_is_empty_should_throw_ArgumentNullException()
        {
            byte[] keyBytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => new AESHelper(keyBytes));
        }

        [Fact]
        public void when_key_is_invalid_should_throw_ArgumentException()
        {
            var keyBytes = new byte[] { 1, 2 };

            Assert.Throws<ArgumentException>(() => new AESHelper(keyBytes));
        }
    }
}