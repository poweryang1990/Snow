using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.IntExtension
{
    public class ToGuidTest
    {
        [Fact]
        public void ToGuid()
        {
            Assert.Equal(Guid.Parse("00000001-0000-0000-0000-000000000000"), 1.ToGuid());
            Assert.Equal(Guid.Parse("0000000F-0000-0000-0000-000000000000"), 15.ToGuid());
            Assert.Equal(Guid.Parse("00000010-0000-0000-0000-000000000000"), 16.ToGuid());
        }
    }
}
