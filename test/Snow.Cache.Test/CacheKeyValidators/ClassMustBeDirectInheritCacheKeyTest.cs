using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Snow.Cache.Test.CacheKeyValidators
{

    public class ClassMustBeDirectInheritCacheKeyTest
    {
        public class MockCacheKey : CacheKey
        {
            public MockCacheKey(string scope) : base(scope)
            {
            }
        }

        public sealed class MockCacheKey2 : MockCacheKey
        {
            private MockCacheKey2() : base("test")
            {
            }
        }

        [Fact]
        public void class_is_not_direct_inherit_CacheKey()
        {
            var cacheKeyTypes = new List<Type>
            {
                typeof(MockCacheKey2)
            };

            Action action = () => CacheKey.Validator.Verify(cacheKeyTypes);

            action.ShouldThrow<Exception>()
                .Where(_ => _.Message.Contains("MockCacheKey2"));
        }
    }
}
