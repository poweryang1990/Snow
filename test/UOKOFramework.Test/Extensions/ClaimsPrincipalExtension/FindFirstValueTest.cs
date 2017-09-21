using System.Security.Claims;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.ClaimsPrincipalExtension
{
    public class FindFirstValueTest
    {
        [Fact]
        public void when_claims_is_null_should_return_null()
        {
            ClaimsPrincipal claimsPrincipal = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var value = claimsPrincipal.FindFirstValue("name");

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_null_should_return_null()
        {
            var claimsPrincipal = new ClaimsPrincipal(new[] {
                new ClaimsIdentity(new[]
                {
                    new Claim("name","chunqiu")
                })
            });

            var value = claimsPrincipal.FindFirstValue(null);

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_not_exist_should_return_null()
        {
            var claimsPrincipal = new ClaimsPrincipal(new[] {
                new ClaimsIdentity(new[]
                {
                    new Claim("name","chunqiu")
                })
            });

            var value = claimsPrincipal.FindFirstValue("age");

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_exist_should_return_value()
        {
            var claimsPrincipal = new ClaimsPrincipal(new[] {
                new ClaimsIdentity(new[]
                {
                    new Claim("name","chunqiu"),
                }),
                new ClaimsIdentity(new[]
                {
                    new Claim("name","chunqiu2")
                })
            });

            var value = claimsPrincipal.FindFirstValue("name");

            Assert.Equal("chunqiu", value);
            Assert.Equal("chunqiu", claimsPrincipal.FindFirstValue("NAME"));
        }
    }
}
