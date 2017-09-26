using System;
using UOKOFramework.Extensions;
using Xunit;
// ReSharper disable ExpressionIsAlwaysNull

namespace UOKOFramework.Test.Extensions.ArrayExtension
{
    public class CombineTest
    {
        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            byte[] bytes1 = null;
            byte[] bytes2 = { 1 };


            Assert.Throws<ArgumentNullException>(() => bytes1.Combine(bytes2));
            Assert.Throws<ArgumentNullException>(() => bytes2.Combine(bytes1));
        }

        [Fact]
        public void when_bytes_is_valid()
        {
            byte[] bytes1 = { 1 };
            byte[] bytes2 = { 2 };

            Assert.Equal(new byte[] { 1, 2 }, bytes1.Combine(bytes2));
            Assert.Equal(new byte[] { 2, 1, 2 }, bytes2.Combine(bytes1).Combine(bytes2));
        }
    }
}
