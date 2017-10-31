using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Snow.Cache.Test.CacheKeyValidators
{

    public class ConstructorsMustBePrivateTest
    {
        public sealed class ConstructorIsPublicClass : CacheKey
        {
            public ConstructorIsPublicClass() : base("test")
            {
            }
        }

        [Fact]
        public void constructor_is_not_private()
        {
            var cacheKeyTypes = new List<Type>
            {
                typeof(ConstructorIsPublicClass)
            };

            Action action = () => CacheKey.Validator.Verify(cacheKeyTypes);

            action.ShouldThrow<Exception>()
                .Where(_ => _.Message.Contains("ConstructorIsPublicClass"));
        }
    }
}
