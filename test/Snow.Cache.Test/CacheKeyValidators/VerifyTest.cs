using System;
using System.Collections.Generic;
using Xunit;

namespace Snow.Cache.Test.CacheKeyValidators
{

    public class VerifyTest
    {
        [Fact]
        public void Verify()
        {
            var cacheKeyTypes = new List<Type>
            {
                typeof(MockCacheKey)
            };

            var result = CacheKey.Validator.Verify(cacheKeyTypes);

            Assert.True(result);
        }
    }
}
