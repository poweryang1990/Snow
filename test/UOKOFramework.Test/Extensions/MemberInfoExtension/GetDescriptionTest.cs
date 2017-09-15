using System.ComponentModel;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.MemberInfoExtension
{
    public class GetDescriptionTest
    {
        [Description("test")]
        public class HasDescriptionClass
        {

        }

        public class SubClassInheritHasDescriptionClass:HasDescriptionClass
        {

        }

        [Fact]
        public void when_member_no_DescriptionAttribute_should_return_empty_string()
        {
            var member = typeof(GetDescriptionTest);

            var description = member.GetDescription();

            Assert.Equal(string.Empty, description);
        }

        [Fact]
        public void when_member_has_DescriptionAttribute_should_return_Description()
        {
            var member = typeof(HasDescriptionClass);

            var description = member.GetDescription();

            Assert.Equal("test", description);
        }

        [Fact]
        public void when_member_no_DescriptionAttribute_but_base_class_have()
        {
            var member = typeof(SubClassInheritHasDescriptionClass);

            Assert.Equal(string.Empty, member.GetDescription());
            Assert.Equal("test", member.GetDescription(true));
        }
    }
}
