using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.StringExtension
{
    public class ToEnumTest
    {
        public enum MockEnum
        {
            No = 0,
            A = 1,
            B = 2
        }

        [Fact]
        public void ToEnum()
        {
            Assert.Equal(MockEnum.A, "A".ToEnum<MockEnum>());
            Assert.Equal(MockEnum.No, "AA".ToEnum<MockEnum>());
            Assert.Equal(MockEnum.B, "AA".ToEnum(MockEnum.B));
        }
    }
}
