using System.Security.Claims;
using System.Threading;
using Snow.Authentication;
using Xunit;

// ReSharper disable ExpressionIsAlwaysNull

namespace Snow.Test.Authentication
{
    public class PrincipalAccessorTest
    {
        [Fact]
        public void when_principal_is_valid()
        {
            Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity("test"));

            IPrincipalAccessor principalAccessor = new PrincipalAccessor();

            Assert.Equal("test", principalAccessor.Principal.Identity.AuthenticationType);
        }
    }
}
