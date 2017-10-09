using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.StringExtension
{
    public class VerifySHA1Test
    {
        [Fact]
        public void when_expectSHA1_is_null_should_throw_ArgumentNullException()
        {
            string expectSHA1 = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => "优客".VerifySHA1(expectSHA1));
        }

        [Fact]
        public void when_expectSHA1_is_invalid_should_throw_ArgumentException()
        {
            var expectSHA1 = "我不是有效的SHA1";

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentException>(() => "优客".VerifySHA1(expectSHA1));
        }

        [Fact]
        public void when_expectSHA1_is_invalid()
        {
            Assert.True("优客".VerifySHA1("E18A69ABFAB46709CA24105B92A1E425BDD75348"));
            Assert.False("优客2".VerifySHA1("E18A69ABFAB46709CA24105B92A1E425BDD75348"));
        }
    }
}
