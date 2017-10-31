using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Snow.Cache.Test.CacheKeyValidators
{

    public class NoDuplicatedScopesTest
    {
        public sealed class MockCacheKey1 : CacheKey
        {
            private MockCacheKey1() : base("test")
            {
            }
        }

        public sealed class MockCacheKey2 : CacheKey
        {
            private MockCacheKey2() : base("test")
            {
            }
        }

        [Fact]
        public void scope_is_duplicated()
        {
            var cacheKeyTypes = new List<Type>
            {
                typeof(MockCacheKey1),
                typeof(MockCacheKey2)
            };

            Action action = () => CacheKey.Validator.Verify(cacheKeyTypes);

            action.ShouldThrow<Exception>()
                .Where(_ => _.Message.Contains("test")
                            && _.Message.Contains("MockCacheKey1")
                            && _.Message.Contains("MockCacheKey2")
                );
        }
    }
}
