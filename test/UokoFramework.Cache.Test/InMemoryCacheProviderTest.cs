using System;
using System.Collections.Generic;
using Xunit;

namespace UOKOFramework.Cache.Test
{

    public class InMemoryCacheProviderTest
    {
        private readonly DateTimeProvider _dateTimeProvider = new DateTimeProvider();


        [Fact]
        public void when_key_not_exist_should_return_null()
        {
            var cacheProvider = new InMemoryCacheProvider(_dateTimeProvider);
            var key = new MockCacheKey().Build("123").Apps;

            var cacheValue = cacheProvider.Get(key);

            Assert.Equal(null, cacheValue);
        }

        [Fact]
        public void when_key_exist_should_retunt_cache()
        {
            var cacheProvider = new InMemoryCacheProvider(_dateTimeProvider);
            var key = new MockCacheKey().Build("123").Apps;
            cacheProvider.Set(key, "abc");

            var cacheValue = cacheProvider.Get(key);

            Assert.Equal("abc", cacheValue);
        }

        [Fact]
        public void when_remove_key_should_retunt_null()
        {
            var cacheProvider = new InMemoryCacheProvider(_dateTimeProvider);
            var key = new MockCacheKey().Build("123").Apps;
            cacheProvider.Set(key, "abc");

            cacheProvider.Remove(key);

            var cacheValue = cacheProvider.Get(key);

            Assert.Equal(null, cacheValue);
        }

        [Fact]
        public void when_remove_key_by_prefix_should_retunt_null()
        {
            var cacheProvider = new InMemoryCacheProvider(_dateTimeProvider);
            var key = new MockCacheKey().Build("123");
            var keyPrefix = key.Prefix;
            cacheProvider.Set(key.Apps, "app");
            cacheProvider.Set(key.Profile, "profile");

            cacheProvider.RemoveByPrefix(keyPrefix);

            var cacheValueOfApps = cacheProvider.Get(key.Apps);
            var cacheValueOfProfile = cacheProvider.Get(key.Profile);
            Assert.Equal(null, cacheValueOfApps);
            Assert.Equal(null, cacheValueOfProfile);
        }

        [Fact]
        public void when_cache_is_timeout_should_retunt_null()
        {
            var cacheProvider = new InMemoryCacheProvider(_dateTimeProvider);
            var key = new MockCacheKey().Build("123").Apps;
            cacheProvider.Set(key, "abc", DateTime.Parse("2017-09-19 16:16:16"));


            _dateTimeProvider.SetNow(DateTime.Parse("2017-09-19 17:16:16"));
            var cacheValue = cacheProvider.Get(key);

            Assert.Equal(null, cacheValue);
        }
    }
}
