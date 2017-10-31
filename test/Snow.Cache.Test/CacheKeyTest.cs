using System;
using Xunit;

namespace Snow.Cache.Test
{

    public class CacheKeyTest
    {

        [Fact]
        public void should_throw_ArgumentNullException_when_only_call_build_but_not_set_name()
        {
            var mockCacheKey = MockCacheKey.Create("123");

            Assert.Throws<ArgumentNullException>(() => mockCacheKey.ToString());
        }

        [Fact]
        public void should_throw_ArgumentNullException_when_call_subClass_ToString_after_set_name()
        {
            var mockCacheKey = MockCacheKey.Create("123");

            var cacheKey = mockCacheKey.Profile;

            Assert.Throws<ArgumentNullException>(() => mockCacheKey.ToString());
            Assert.Equal("mock:&user-id=123#profile", cacheKey.ToString());
        }

        [Fact]
        public void should_get_key_include_param_and_name_when_has_param_and_has_name()
        {
            var mockCacheKey = MockCacheKey.Create("123").Apps;

            var key = mockCacheKey.ToString();

            Assert.Equal("mock:&user-id=123#apps", key);
        }

        [Fact]
        public void should_get_right_sequencekey_key_when_call_build_after_set_name()
        {
            var cacheKey = MockCacheKey.Create("123", "abc").Apps;


            Assert.Equal("mock:&user-id=123&client-id=abc#apps", cacheKey.ToString());
        }
    }
}
