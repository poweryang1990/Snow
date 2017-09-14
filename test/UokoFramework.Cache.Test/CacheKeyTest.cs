using System;
using System.Collections.Generic;
using Xunit;

namespace UOKOFramework.Cache.Test
{

    public class CacheKeyTest
    {
        public class SubCacheKey : CacheKey
        {
            // 正常情况下应该是privated的只读的常量，这里设置为外界可写为了测试。
            public static string Scope;

            // 正常情况下应该是private，这里设置为public是为了更直观的测试。
            public class Params
            {
                // 正常情况下应该是只读的常量，这里设置为外界可写为了更直观的测试。
                public static string UserId;
                public static string ClientId;
            }

            // 正常情况下应该是private，这里设置为public是为了更直观的测试。
            public class Names
            {
                // 正常情况下应该是只读的常量，这里设置为外界可写为了更直观的测试。
                public static string Empty;
                public static string Apps;
                public static string Profile;
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

            public CacheKey Empty => base.Clone(Names.Empty);
        }

        [Fact]
        public void should_throw_ArgumentNullException_when_not_set_name()
        {
            SubCacheKey.Scope = "test";
            var subCacheKey = new SubCacheKey();

            Assert.Throws<ArgumentNullException>(() => subCacheKey.ToString());
        }

        [Fact]
        public void should_throw_ArgumentNullException_when_only_call_build_but_not_set_name()
        {
            SubCacheKey.Scope = "test";
            SubCacheKey.Params.UserId = "user-id";
            const string userId = "123";
            var subCacheKey = new SubCacheKey();

            var cacheKey = subCacheKey.Build(userId);

            Assert.Throws<ArgumentNullException>(() => cacheKey.ToString());
        }

        [Fact]
        public void should_throw_ArgumentNullException_when_call_subClass_ToString_after_set_name()
        {
            SubCacheKey.Scope = "test";
            SubCacheKey.Params.UserId = "user-id";
            SubCacheKey.Names.Profile = "profile";
            const string userId = "123";
            var subCacheKey = new SubCacheKey().Build(userId);

            var cacheKey = subCacheKey.Profile;

            Assert.Throws<ArgumentNullException>(() => subCacheKey.ToString());
            Assert.Equal("test:&user-id=123#profile", cacheKey.ToString());
        }

        [Fact]
        public void should_no_include_name_when_name_is_empty()
        {
            SubCacheKey.Scope = "test";
            SubCacheKey.Names.Empty = string.Empty;
            var cacheKey = new SubCacheKey().Empty;

            var key = cacheKey.ToString();

            Assert.Equal("test:#", key);
        }

        [Fact]
        public void should_get_key_include_param_and_name_when_has_param_and_has_name()
        {
            SubCacheKey.Scope = "test";
            SubCacheKey.Params.UserId = "user-id";
            SubCacheKey.Names.Apps = "apps";
            const string userId = "123";
            var cacheKey = new SubCacheKey().Build(userId).Apps;

            var key = cacheKey.ToString();

            Assert.Equal("test:&user-id=123#apps", key);
        }

        [Fact]
        public void should_use_last_build_param_when_call_twice_build()
        {
            SubCacheKey.Scope = "test";
            SubCacheKey.Params.UserId = "user-id";
            SubCacheKey.Params.ClientId = "client-id";
            SubCacheKey.Names.Apps = "apps";
            const string userId = "123";
            const string clientId = "abc";
            var subCacheKey = new SubCacheKey();

            var cacheKey = subCacheKey.Build(userId, clientId).Build(userId).Apps;

            Assert.Equal("test:&user-id=123#apps", cacheKey.ToString());
        }

        [Fact]
        public void should_use_last_name_when_set_twice_name()
        {
            SubCacheKey.Scope = "test";
            SubCacheKey.Params.UserId = "user-id";
            SubCacheKey.Names.Apps = "apps";
            SubCacheKey.Names.Profile = "profile";
            var subCacheKey = new SubCacheKey();

            var cacheKey = ((SubCacheKey)subCacheKey.Apps).Profile;

            Assert.Equal("test:#profile", cacheKey.ToString());
        }

        [Fact]
        public void should_get_right_sequencekey_key_when_call_build_after_set_name()
        {
            SubCacheKey.Scope = "test";
            SubCacheKey.Params.UserId = "user-id";
            SubCacheKey.Names.Apps = "apps";
            const string userId = "123";
            var subCacheKey = new SubCacheKey();

            var cacheKey = ((SubCacheKey)subCacheKey.Apps).Build(userId);

            Assert.Equal("test:&user-id=123#apps", cacheKey.ToString());
        }
    }
}
