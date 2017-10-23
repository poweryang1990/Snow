using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.StringExtension
{
    public class ToEnumOrNullTest
    {
        public enum MockEnum
        {
            No = 0,
            A = 1,
            B = 2
        }

        [Fact]
        public void ToEnumOrNull()
        {
            Assert.Equal(MockEnum.A, "A".ToEnumOrNull<MockEnum>());
            Assert.Null("AA".ToEnumOrNull<MockEnum>());
        }
    }
}
