using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.GuidExtension
{
    public class IsEmptyTest
    {
        [Fact]
        public void IsEmpty()
        {
            Assert.True(Guid.Empty.IsEmpty());
            Assert.True(((Guid?)null).IsEmpty());
            Assert.False(Guid.Parse("10000000-0000-0000-0000-000000000000").IsEmpty());
        }
    }
}
