using System.Collections.Generic;
using System.Security.Claims;
using Xunit;
using Snow.Extensions;

namespace Snow.Test.Extensions.ClaimExtension
{
    public class FindFirstValueTest
    {
        [Fact]
        public void when_claims_is_null_should_return_null()
        {
            var claims = (IEnumerable<Claim>)null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var value = claims.FindFirstValue("name");

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_null_should_return_null()
        {
            var claims = new[]
            {
                new Claim("name","chunqiu")
            };

            var value = claims.FindFirstValue(null);

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_not_exist_should_return_null()
        {
            var claims = new[]
            {
                new Claim("name","chunqiu")
            };

            var value = claims.FindFirstValue("age");

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_exist_should_return_value()
        {
            var claims = new[]
            {
                new Claim("name","chunqiu"),
                new Claim("name","chunqiu2")
            };

            var value = claims.FindFirstValue("name");

            Assert.Equal("chunqiu", value);
            Assert.Equal("chunqiu", claims.FindFirstValue("NAME"));
        }
    }
}
