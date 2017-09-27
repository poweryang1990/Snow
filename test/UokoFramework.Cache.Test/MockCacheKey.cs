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
            base.SetParams("user-id", userId);
            return this;
        }

        public MockCacheKey Build(string userId, string clientId)
        {
            base.SetParams(new Dictionary<string, string>
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