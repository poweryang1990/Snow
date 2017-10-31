using System;
using Xunit;

namespace Snow.Cache.Test
{

    public class CacheKeyTest
    {

        [Fact]
        public void should_throw_ArgumentNullException_when_not_set_name()
        {
            var mockCacheKey = MockCacheKey.Create("123");

            Assert.Throws<ArgumentNullException>(() => mockCacheKey.ToString());
        }

        [Fact]
        public void should_get_key_include_param_and_name_when_set_param_and_name()
        {
            var mockCacheKey = MockCacheKey.Create("123").Apps;

            var key = mockCacheKey.ToString();

            Assert.Equal("mock:&user-id=123#apps", key);
        }

        [Fact]
        public void should_get_right_sequencekey_key_when_set_two_params()
        {
            var cacheKey = MockCacheKey.Create("123", "abc").Apps;


            Assert.Equal("mock:&user-id=123&client-id=abc#apps", cacheKey.ToString());
        }

        [Fact]
        public void should_get_key_when_not_set_param()
        {
            var cacheKey = MockCacheKey.Create().Apps;


            Assert.Equal("mock:#apps", cacheKey.ToString());
        }
    }
}
