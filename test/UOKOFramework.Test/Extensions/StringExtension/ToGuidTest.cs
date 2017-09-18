using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.StringExtension
{
    public class ToGuidTest
    {
        [Fact]
        public void ToGuid()
        {
            Assert.Equal(Guid.Empty, ((string)null).ToGuid());
            Assert.Equal(Guid.Empty, "A".ToGuid());
            Assert.Equal(Guid.Parse("12345678-1234-5678-abcd-123456789abc"), "12345678-1234-5678-abcd-123456789abc".ToGuid());
        }
    }
}
