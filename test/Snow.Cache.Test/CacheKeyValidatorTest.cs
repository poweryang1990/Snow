using System;
using System.Collections.Generic;
using Xunit;

namespace Snow.Cache.Test
{

    public class CacheKeyValidatorTest
    {
        [Fact]
        public void Validate()
        {
            var cacheKeyValidator = new CacheKey.Validator();
            var cacheKeyTypes = new List<Type>
            {
                typeof(MockCacheKey)
            };

            var result = cacheKeyValidator.Validate(cacheKeyTypes);

            Assert.True(result);
        }
    }
}
