using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.StringExtension
{
    public class IsEqualTest
    {
        [Fact]
        public void IsEqual()
        {
            Assert.True(((string)null).IsEqual(null));
            Assert.False("".IsEqual(null));
            Assert.True("".IsEqual(""));
            Assert.True("a".IsEqual("A"));
            Assert.True("aa".IsEqual("Aa"));
            Assert.False("aa".IsEqual("Aac"));
        }
    }
}
