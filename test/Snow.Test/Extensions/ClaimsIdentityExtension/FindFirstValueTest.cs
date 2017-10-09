using System.Security.Claims;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.ClaimsIdentityExtension
{
    public class FindFirstValueTest
    {
        [Fact]
        public void when_claims_is_null_should_return_null()
        {
            ClaimsIdentity claimsIdentity = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var value = claimsIdentity.FindFirstValue("name");

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_null_should_return_null()
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("name","chunqiu")
            });

            var value = claimsIdentity.FindFirstValue(null);

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_not_exist_should_return_null()
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("name","chunqiu")
            });

            var value = claimsIdentity.FindFirstValue("age");

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_exist_should_return_value()
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("name","chunqiu"),
                new Claim("name","chunqiu2")
            });

            var value = claimsIdentity.FindFirstValue("name");

            Assert.Equal("chunqiu", value);
            Assert.Equal("chunqiu", claimsIdentity.FindFirstValue("NAME"));
        }
    }
}
