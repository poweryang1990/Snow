using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Snow.Cache.Test.CacheKeyValidators
{

    public class NoDuplicatedNamesTest
    {
        public sealed class MockCacheKey1 : CacheKey
        {
            private MockCacheKey1() : base("test")
            {
            }

            public CacheKey App => base.Clone("app");

            public CacheKey App2 => base.Clone("app");

        }

        [Fact]
        public void name_is_duplicated()
        {
            var cacheKeyTypes = new List<Type>
            {
                typeof(MockCacheKey1)
            };

            Action action = () => CacheKey.Validator.Verify(cacheKeyTypes);

            action.ShouldThrow<Exception>()
                .Where(_ => _.Message.Contains("app")
                            && _.Message.Contains("MockCacheKey1")
                );
        }
    }
}
