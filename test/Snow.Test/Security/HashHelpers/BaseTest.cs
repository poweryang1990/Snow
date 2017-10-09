using System;
using Snow.Extensions;
using Snow.Security;
using Snow.Text;
using Xunit;

namespace Snow.Test.Security.HashHelpers
{
    public class BaseTest
    {
        public HashHelper BuildHashHelper(string value)
        {
            var bytes = value.GetBytes();
            return new HashHelper(bytes);
        }

        public string GetHex(byte[] bytes)
        {
            var byteHelper = new ByteHelper();

            return byteHelper.GetHex(bytes, withHyphen: false, lowerCase: false);
        }

        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => new HashHelper(bytes));
        }

        [Fact]
        public void when_bytes_is_empty_should_throw_ArgumentNullException()
        {
            byte[] bytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => new HashHelper(bytes));
        }
    }
}