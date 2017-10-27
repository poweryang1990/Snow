using System;
using Snow.Cryptography;
using Snow.Extensions;
using Snow.Text;
using Xunit;

namespace Snow.Test.Cryptography.HashProviders
{
    public class BaseTest
    {
        public HashProvider BuildHashProvider(string value)
        {
            var bytes = value.GetBytes();
            return HashProvider.New(bytes);
        }

        public string GetHex(byte[] bytes)
        {
            var byteHelper = ByteHelper.New();

            return byteHelper.GetHex(bytes, withHyphen: false, lowerCase: false);
        }

        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => HashProvider.New(bytes));
        }

        [Fact]
        public void when_bytes_is_empty_should_throw_ArgumentNullException()
        {
            byte[] bytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => HashProvider.New(bytes));
        }
    }
}