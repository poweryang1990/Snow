using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Snow.Cache.Test.CacheKeyValidators
{

    public class ClassMustBeSealedTest
    {
        public class NotSealedClass : CacheKey
        {
            private NotSealedClass() : base("test")
            {
            }
        }

        [Fact]
        public void class_is_not_sealed()
        {
            var cacheKeyTypes = new List<Type>
            {
                typeof(NotSealedClass)
            };

            Action action = () => CacheKey.Validator.Verify(cacheKeyTypes);

            action.ShouldThrow<Exception>()
                .Where(_ => _.Message.Contains("NotSealedClass"));
        }
    }
}
