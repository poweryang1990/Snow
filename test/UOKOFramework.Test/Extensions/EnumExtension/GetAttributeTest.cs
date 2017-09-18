using UOKOFramework.Attributes;
using Xunit;
using UOKOFramework.Extensions;

namespace UOKOFramework.Test.Extensions.EnumExtension
{
    public class GetAttributeTest
    {
        public enum MockEnum
        {
            [Group("test")]
            A = 1,
            B = 2
        }

        [Fact]
        public void GetAttribute()
        {
            var groupAttribute = MockEnum.A.GetAttribute<GroupAttribute>();

            Assert.Equal("test", groupAttribute.Name);
            Assert.Equal(typeof(GroupAttribute), groupAttribute.GetType());

            Assert.Equal(null, MockEnum.B.GetAttribute<GroupAttribute>());
        }
    }
}
