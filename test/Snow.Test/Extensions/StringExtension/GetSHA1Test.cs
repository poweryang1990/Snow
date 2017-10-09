using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.StringExtension
{
    public class GetSHA1Test
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            string value = null;

            Assert.Throws<ArgumentNullException>(() => value.GetSHA1());
        }

        [Fact]
        public void when_value_is_not_null()
        {
            Assert.Equal("E18A69ABFAB46709CA24105B92A1E425BDD75348", "优客".GetSHA1());
        }
    }
}
