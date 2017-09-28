using System;
using UOKOFramework.Cache.Memory;
using Xunit;

namespace UOKOFramework.Cache.Test
{

    public class MemoryCacheTest
    {
        private readonly Clock _clock = new Clock();

        private IMemoryCache BuildMemoryCache()
        {
            return new MemoryCache(_clock);
        }

        [Fact]
        public void when_key_not_exist_should_return_null()
        {
            var memoryCache = BuildMemoryCache();
            var key = new MockCacheKey().SetParams("123").Apps;

            var cacheValue = memoryCache.Get(key);

            Assert.Equal(null, cacheValue);
        }

        [Fact]
        public void when_key_exist_should_retunt_cache()
        {
            var memoryCache = BuildMemoryCache();
            var key = new MockCacheKey().SetParams("123").Apps;
            memoryCache.Set(key, "abc");

            var cacheValue = memoryCache.Get(key);

            Assert.Equal("abc", cacheValue);
        }

        [Fact]
        public void when_remove_key_should_retunt_null()
        {
            var memoryCache = BuildMemoryCache();
            var key = new MockCacheKey().SetParams("123").Apps;
            memoryCache.Set(key, "abc");

            memoryCache.Delete(key);

            var cacheValue = memoryCache.Get(key);

            Assert.Equal(null, cacheValue);
        }

        [Fact]
        public void when_remove_key_by_prefix_should_retunt_null()
        {
            var memoryCache = BuildMemoryCache();
            var key = new MockCacheKey().SetParams("123");
            var keyPrefix = key.Prefix;
            memoryCache.Set(key.Apps, "app");
            memoryCache.Set(key.Profile, "profile");

            memoryCache.Delete(keyPrefix, true);

            var cacheValueOfApps = memoryCache.Get(key.Apps);
            var cacheValueOfProfile = memoryCache.Get(key.Profile);
            Assert.Equal(null, cacheValueOfApps);
            Assert.Equal(null, cacheValueOfProfile);
        }

        [Fact]
        public void when_cache_is_timeout_should_retunt_null()
        {
            _clock.Now = DateTimeOffset.Parse("2017-09-19 17:16:16");

            var memoryCache = BuildMemoryCache();
            var key = new MockCacheKey().SetParams("123").Apps;
            memoryCache.Set(key, "abc", TimeSpan.FromHours(1));

            
            _clock.Now = DateTimeOffset.Parse("2017-09-20 17:16:16");
            var cacheValue = memoryCache.Get(key);

            Assert.Equal(null, cacheValue);
        }


    }
}
