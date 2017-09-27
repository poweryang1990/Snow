using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.StringExtension
{
    public class IncludeTest
    {
        [Fact]
        public void when_original_string_is_null()
        {
            Assert.False(((string)null).Include("a"));
        }

        [InlineData(null)]
        [InlineData("")]
        [Theory]
        public void when_string_is_invalid(string value)
        {
            Assert.False("a".Include(value));
        }

        [InlineData("a")]
        [InlineData("A")]
        [InlineData("Ab")]
        [Theory]
        public void when_string_valid(string value)
        {
            Assert.True("abc".Include(value));
        }
    }
}
