using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.GuidExtension
{
    public class ToInt32Test
    {
        [Fact]
        public void ToInt32()
        {
            Assert.Equal(0,Guid.Empty.ToInt32());
            Assert.Equal(1,Guid.Parse("00000001-0000-0000-0000-000000000000").ToInt32());
            Assert.Equal(15,Guid.Parse("0000000F-0000-0000-0000-000000000000").ToInt32());
            Assert.Equal(16,Guid.Parse("00000010-0000-0000-0000-000000000000").ToInt32());
        }
    }
}
