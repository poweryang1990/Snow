using System.Collections.Generic;

namespace Snow.Cache.Test
{
    public sealed class MockCacheKey : CacheKey
    {
        private MockCacheKey() : base("mock")
        {

        }

        public static MockCacheKey Create(string userId)
        {
            var mockCacheKey = new MockCacheKey();
            mockCacheKey.SetParams("user-id", userId);
            return mockCacheKey;
        }

        public static MockCacheKey Create(string userId, string clientId)
        {
            var mockCacheKey = new MockCacheKey();
            mockCacheKey.SetParams(new Dictionary<string, string>
            {
                ["user-id"] = userId,
                ["client-id"] = clientId,
            });
            return mockCacheKey;
        }

        public CacheKey Prefix => base.Clone("");

        public CacheKey Profile => base.Clone("profile");

        public CacheKey Apps => base.Clone("apps");
    }
}