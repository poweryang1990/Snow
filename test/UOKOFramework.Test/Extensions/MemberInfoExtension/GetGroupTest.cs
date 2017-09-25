using System.ComponentModel;
using UOKOFramework.Attributes;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.MemberInfoExtension
{
    public class GetGroupTest
    {
        [Group("test")]
        public class HasGroupClass
        {

        }

        public class SubClassInheritHasGroupClass : HasGroupClass
        {

        }

        [Fact]
        public void when_member_no_DescriptionAttribute_should_return_empty_string()
        {
            var member = typeof(GetGroupTest);

            var description = member.GetGroupName();

            Assert.Null(description);
        }

        [Fact]
        public void when_member_has_GroupAttribute_should_return_Description()
        {
            var member = typeof(HasGroupClass);

            var description = member.GetGroupName();

            Assert.Equal("test", description);
        }

        [Fact]
        public void when_member_no_GroupAttribute_but_base_class_have()
        {
            var member = typeof(SubClassInheritHasGroupClass);

            Assert.Null(member.GetGroupName());
            Assert.Equal("test", member.GetGroupName(true));
        }
    }
}
