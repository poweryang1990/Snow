using System.Collections.Generic;
using System.Security.Claims;
using Xunit;
using Snow.Extensions;

namespace Snow.Test.Extensions.ClaimExtension
{
    public class FindAllValueTest
    {
        [Fact]
        public void when_claims_is_null_should_return_null()
        {
            var claims = (IEnumerable<Claim>)null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var value = claims.FindAllValues("name");

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_null_should_return_null()
        {
            var claims = new[]
            {
                new Claim("name","chunqiu")
            };

            var value = claims.FindAllValues(null);

            Assert.Equal(new string[0],value);
        }

        [Fact]
        public void when_claim_type_is_not_exist_should_return_null()
        {
            var claims = new[]
            {
                new Claim("name","chunqiu")
            };

            var value = claims.FindAllValues("age");

            Assert.Equal(new string[0], value);
        }

        [Fact]
        public void when_claim_type_is_exist_should_return_value()
        {
            var claims = new[]
            {
                new Claim("name","chunqiu"),
                new Claim("name","uoko")
            };

            var value = claims.FindAllValues("name");

            var expected = new[] { "chunqiu", "uoko" };
            Assert.Equal(expected, value);
            Assert.Equal(expected, claims.FindAllValues("NAME"));
        }
    }
}
