using System;
using Xunit;

namespace Snow.Cache.Test
{

    public class CacheKeyTest
    {
        [Fact]
        public void should_throw_ArgumentNullException_when_not_set_name()
        {
            var mockCacheKey = new MockCacheKey();

            Assert.Throws<ArgumentNullException>(() => mockCacheKey.ToString());
        }

        [Fact]
        public void should_throw_ArgumentNullException_when_only_call_build_but_not_set_name()
        {
            var mockCacheKey = new MockCacheKey();

            var cacheKey = mockCacheKey.SetParams("123");

            Assert.Throws<ArgumentNullException>(() => cacheKey.ToString());
        }

        [Fact]
        public void should_throw_ArgumentNullException_when_call_subClass_ToString_after_set_name()
        {
            var mockCacheKey = new MockCacheKey()
                .SetParams("123");

            var cacheKey = mockCacheKey.Profile;

            Assert.Throws<ArgumentNullException>(() => mockCacheKey.ToString());
            Assert.Equal("mock:&user-id=123#profile", cacheKey.ToString());
        }

        [Fact]
        public void should_no_include_name_when_name_is_empty()
        {
            var mockCacheKey = new MockCacheKey().Prefix;

            var key = mockCacheKey.ToString();

            Assert.Equal("mock:#", key);
        }

        [Fact]
        public void should_get_key_include_param_and_name_when_has_param_and_has_name()
        {
            var mockCacheKey = new MockCacheKey()
                .SetParams("123")
                .Apps;

            var key = mockCacheKey.ToString();

            Assert.Equal("mock:&user-id=123#apps", key);
        }

        [Fact]
        public void should_use_last_build_param_when_call_twice_build()
        {
            var mockCacheKey = new MockCacheKey();

            var cacheKey = mockCacheKey
                .SetParams("123", "abc")
                .SetParams("123")
                .Apps;

            Assert.Equal("mock:&user-id=123#apps", cacheKey.ToString());
        }

        [Fact]
        public void should_get_right_sequencekey_key_when_call_build_after_set_name()
        {
            var mockCacheKey = new MockCacheKey();

            var cacheKey = ((MockCacheKey)mockCacheKey.Apps)
                .SetParams("123", "abc");

            Assert.Equal("mock:&user-id=123&client-id=abc#apps", cacheKey.ToString());
        }

        [Fact]
        public void should_use_last_name_when_set_twice_name()
        {
            var mockCacheKey = new MockCacheKey();

            var cacheKey = ((MockCacheKey)mockCacheKey.Apps).Profile;

            Assert.Equal("mock:#profile", cacheKey.ToString());
        }
    }
}
