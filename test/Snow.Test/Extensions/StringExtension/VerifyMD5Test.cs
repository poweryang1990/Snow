using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.StringExtension
{
    public class VerifyMD5Test
    {
        [Fact]
        public void when_expectMd5_is_null_should_throw_ArgumentNullException()
        {
            string expectMd5 = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => "优客".VerifyMD5(expectMd5));
        }

        [Fact]
        public void when_expectMd5_is_invalid_should_throw_ArgumentException()
        {
            var expectMd5 = "我不是有效的MD5";

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentException>(() => "优客".VerifyMD5(expectMd5));
        }

        [Fact]
        public void when_expectMd5_is_invalid()
        {
            Assert.True("优客".VerifyMD5("0E8869D60C581C8A86DB3B7D3992BF11"));
            Assert.False("优客2".VerifyMD5("0E8869D60C581C8A86DB3B7D3992BF11"));
        }
    }
}
