using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.StringExtension
{
    public class DefaultTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void when_value_is_null_or_emptry(string value)
        {
            Assert.Equal("default", value.Default("default"));
        }

        [Fact]
        public void when_value_is_valid()
        {
            Assert.Equal("abc", "abc".Default("default"));
        }
    }
}
