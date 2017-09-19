using System.Collections.Generic;

namespace UOKOFramework.Cache.Test
{
    public class SubCacheKey : CacheKey
    {
        // 正常情况下应该是privated的只读的常量，这里设置为外界可写为了测试。
        public static string Scope = "test";

        // 正常情况下应该是private，这里设置为public是为了更直观的测试。
        public class Params
        {
            // 正常情况下应该是只读的常量，这里设置为外界可写为了更直观的测试。
            public static string UserId = "user-id";
            public static string ClientId = "client-id";
        }

        // 正常情况下应该是private，这里设置为public是为了更直观的测试。
        public class Names
        {
            // 正常情况下应该是只读的常量，这里设置为外界可写为了更直观的测试。
            public static string Empty = "";
            public static string Apps = "apps";
            public static string Profile = "profile";
        }

        public SubCacheKey() : base(Scope)
        {

        }

        public SubCacheKey Build(string userId)
        {
            return base.Build<SubCacheKey>(Params.UserId, userId);
        }

        public SubCacheKey Build(string userId, string clientId)
        {
            return base.Build<SubCacheKey>(new Dictionary<string, string>
            {
                [Params.UserId] = userId,
                [Params.ClientId] = clientId,
            });
        }

        public CacheKey Profile => base.Clone(Names.Profile);

        public CacheKey Apps => base.Clone(Names.Apps);

        public CacheKey Prefix => base.Clone(Names.Empty);
    }
}