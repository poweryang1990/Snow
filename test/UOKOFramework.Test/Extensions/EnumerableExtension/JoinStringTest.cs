using System.Collections.Generic;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.EnumerableExtension
{
    public class JoinStringTest
    {
        public class MockClass
        {
            public string Name { get; set; }
        }

        [Fact]
        public void when_this_is_null_should_return_string_empty()
        {
            Assert.Equal(string.Empty,((IEnumerable<string>)null).JoinString("-"));
            Assert.Equal(string.Empty, ((IEnumerable<JoinStringTest>)null).JoinString("-"));
            Assert.Equal(string.Empty, ((IEnumerable<JoinStringTest>)null).JoinString("-", item => item.ToString()));
        }

        [Fact]
        public void when_this_is_valid()
        {
            var stringList = new[] { "A", "B" };
            var objectList = new[]
            {
                new MockClass{Name="AClass"},
                new MockClass{Name="BClass"}
            };

            var mockClassType = typeof(MockClass);

            Assert.Equal("A-B", stringList.JoinString("-"));
            Assert.Equal(mockClassType.FullName + "+" + mockClassType.FullName, objectList.JoinString("+"));
            Assert.Equal("AClass-BClass", objectList.JoinString("-", item => item.Name));
        }
    }
}
