using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Snow.Cache.Test.CacheKeyValidators
{

    public class PublicPropertiesTypeMustBeCacheKeyTest
    {
        public sealed class MockCacheKey1 : CacheKey
        {
            private MockCacheKey1() : base("test")
            {
            }

            public CacheKey Name => base.Clone("name");

            public MockCacheKey1 App => (MockCacheKey1)base.Clone("app");

            public static MockCacheKey1 UserId => (MockCacheKey1)(new MockCacheKey1().Clone("user-id"));

        }

        [Fact]
        public void public_properties_type_is_not_CacheKey()
        {
            var cacheKeyTypes = new List<Type>
            {
                typeof(MockCacheKey1)
            };

            Action action = () => CacheKey.Validator.Verify(cacheKeyTypes);

            action.ShouldThrow<Exception>()
                .Where(_ => _.Message.Contains("App")
                            && _.Message.Contains("UserId")
                            && _.Message.Contains("MockCacheKey1")
                );
        }
    }
}
