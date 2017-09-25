using System;
using UOKOFramework.Extensions;
using UOKOFramework.Security;
using Xunit;

// ReSharper disable InconsistentNaming

namespace UOKOFramework.Test.Security.ASCIIHelpers
{
    public class BaseTest
    {
        public ASCIIHelper BuildASCIIHelper(string value)
        {
            var bytes = value.GetBytes();
            return new ASCIIHelper(bytes);
        }

        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => new ASCIIHelper(bytes));
        }

        [Fact]
        public void when_bytes_is_empty_should_throw_ArgumentNullException()
        {
            byte[] bytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => new ASCIIHelper(bytes));
        }
    }
}