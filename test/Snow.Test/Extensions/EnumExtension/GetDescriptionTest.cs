using System.ComponentModel;
using Xunit;
using Snow.Extensions;

namespace Snow.Test.Extensions.EnumExtension
{
    public class GetDescriptionTest
    {
        public enum MockEnum
        {
            [Description("答案是A")]
            A = 1,
            B = 2
        }

        [Fact]
        public void GetDescription()
        {
            Assert.Equal("答案是A", MockEnum.A.GetDescription());
            Assert.Equal("B", MockEnum.B.GetDescription());
        }
    }
}
