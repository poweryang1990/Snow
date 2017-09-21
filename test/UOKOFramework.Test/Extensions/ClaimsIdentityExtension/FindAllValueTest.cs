using System.Security.Claims;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.ClaimsIdentityExtension
{
    public class FindAllValueTest
    {
        [Fact]
        public void when_claims_is_null_should_return_null()
        {
            ClaimsIdentity claimsIdentity = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var value = claimsIdentity.FindAllValues("name");

            Assert.Null(value);
        }

        [Fact]
        public void when_claim_type_is_null_should_return_null()
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("name","chunqiu")
            });

            var value = claimsIdentity.FindAllValues(null);

            Assert.Equal(new string[0], value);
        }

        [Fact]
        public void when_claim_type_is_not_exist_should_return_null()
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("name","chunqiu")
            });

            var value = claimsIdentity.FindAllValues("age");

            Assert.Equal(new string[0], value);
        }

        [Fact]
        public void when_claim_type_is_exist_should_return_value()
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("name","chunqiu"),
                new Claim("name","uoko")
            });

            var value = claimsIdentity.FindAllValues("name");

            var expected = new[] { "chunqiu", "uoko" };
            Assert.Equal(expected, value);
            Assert.Equal(expected, claimsIdentity.FindAllValues("NAME"));
        }
    }
}
