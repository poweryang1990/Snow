using System.Collections.Generic;

namespace Snow.Cache.Test
{
    public sealed class MockCacheKey : CacheKey
    {
        internal MockCacheKey() : base("mock")
        {

        }

        public MockCacheKey SetParams(string userId)
        {
            base.SetParamsCore("user-id", userId);
            return this;
        }

        public MockCacheKey SetParams(string userId, string clientId)
        {
            base.SetParamsCore(new Dictionary<string, string>
            {
                ["user-id"] = userId,
                ["client-id"] = clientId,
            });
            return this;
        }

        public CacheKey Prefix => base.Clone("");

        public CacheKey Profile => base.Clone("profile");

        public CacheKey Apps => base.Clone("apps");
    }
}