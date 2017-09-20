using System;
using UOKOFramework.Cache.Memory;
using Xunit;

namespace UOKOFramework.Cache.Test
{

    public class MemoryCacheTest
    {
        private readonly DateTimeProvider _dateTimeProvider = new DateTimeProvider();

        private MemoryCache BuildMemoryCache()
        {
            return new MemoryCache(_dateTimeProvider);
        }

        [Fact]
        public void when_key_not_exist_should_return_null()
        {
            var memoryCache = BuildMemoryCache();
            var key = new MockCacheKey().Build("123").Apps;

            var cacheValue = memoryCache.Get(key);

            Assert.Equal(null, cacheValue);
        }

        [Fact]
        public void when_key_exist_should_retunt_cache()
        {
            var memoryCache = BuildMemoryCache();
            var key = new MockCacheKey().Build("123").Apps;
            memoryCache.Set(key, "abc");

            var cacheValue = memoryCache.Get(key);

            Assert.Equal("abc", cacheValue);
        }

        [Fact]
        public void when_remove_key_should_retunt_null()
        {
            var memoryCache = BuildMemoryCache();
            var key = new MockCacheKey().Build("123").Apps;
            memoryCache.Set(key, "abc");

            memoryCache.Remove(key);

            var cacheValue = memoryCache.Get(key);

            Assert.Equal(null, cacheValue);
        }

        [Fact]
        public void when_remove_key_by_prefix_should_retunt_null()
        {
            var memoryCache = BuildMemoryCache();
            var key = new MockCacheKey().Build("123");
            var keyPrefix = key.Prefix;
            memoryCache.Set(key.Apps, "app");
            memoryCache.Set(key.Profile, "profile");

            memoryCache.RemoveByPrefix(keyPrefix);

            var cacheValueOfApps = memoryCache.Get(key.Apps);
            var cacheValueOfProfile = memoryCache.Get(key.Profile);
            Assert.Equal(null, cacheValueOfApps);
            Assert.Equal(null, cacheValueOfProfile);
        }

        [Fact]
        public void when_cache_is_timeout_should_retunt_null()
        {
            var memoryCache = BuildMemoryCache();
            var key = new MockCacheKey().Build("123").Apps;
            memoryCache.Set(key, "abc", DateTime.Parse("2017-09-19 16:16:16"));


            _dateTimeProvider.SetNow(DateTime.Parse("2017-09-19 17:16:16"));
            var cacheValue = memoryCache.Get(key);

            Assert.Equal(null, cacheValue);
        }

        
    }
}
