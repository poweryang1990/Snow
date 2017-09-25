using System.Collections.Generic;

namespace UOKOFramework.Cache.Test
{
    public class MockCacheKey : CacheKey
    {
        internal MockCacheKey() : base("mock")
        {

        }

        public MockCacheKey Build(string userId)
        {
            return base.Build<MockCacheKey>("user-id", userId);
        }

        public MockCacheKey Build(string userId, string clientId)
        {
            return base.Build<MockCacheKey>(new Dictionary<string, string>
            {
                ["user-id"] = userId,
                ["client-id"] = clientId,
            });
        }

        public CacheKey Prefix => base.Clone("");

        public CacheKey Profile => base.Clone("profile");

        public CacheKey Apps => base.Clone("apps");
    }
}