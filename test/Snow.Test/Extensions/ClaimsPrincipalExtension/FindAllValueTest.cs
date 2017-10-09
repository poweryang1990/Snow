using System.Security.Claims;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.ClaimsPrincipalExtension
{
    public class FindAllValueTest
    {
        [Fact]
        public void when_claims_is_null_should_return_null()
        {
            ClaimsPrincipal claimsPrincipal = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var value = claimsPrincipal.FindAllValues("name");

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_null_should_return_null()
        {
            var claimsPrincipal = new ClaimsPrincipal(new[] {
                new ClaimsIdentity(new[]
                {
                    new Claim("name","chunqiu"),
                })
            });

            var value = claimsPrincipal.FindAllValues(null);

            Assert.Equal(new string[0], value);
        }

        [Fact]
        public void when_claim_type_is_not_exist_should_return_null()
        {
            var claimsPrincipal = new ClaimsPrincipal(new[] {
                new ClaimsIdentity(new[]
                {
                    new Claim("name","chunqiu"),
                })
            });

            var value = claimsPrincipal.FindAllValues("age");

            Assert.Equal(new string[0], value);
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
                    new Claim("name","uoko")
                })
            });

            var value = claimsPrincipal.FindAllValues("name");

            var expected = new[] { "chunqiu", "uoko" };
            Assert.Equal(expected, value);
            Assert.Equal(expected, claimsPrincipal.FindAllValues("NAME"));
        }
    }
}
