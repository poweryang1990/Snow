using System;
using System.Security.Claims;
using Moq;
using UOKOFramework.Security;
using Xunit;
// ReSharper disable ExpressionIsAlwaysNull

namespace UOKOFramework.Test.Security
{
    public class IdentityAccessorTest
    {
        [Fact]
        public void when_IPrincipalAccessor_is_null_should_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new IdentityAccessor(null));
        }

        [Fact]
        public void when_principal_is_null_should_return_null()
        {
            ClaimsPrincipal claimsPrincipal = null;
            var mockIPrincipalAccessor = MockIPrincipalAccessor(claimsPrincipal);
            IIdentityAccessor identityAccessor = new IdentityAccessor(mockIPrincipalAccessor.Object);

            Assert.Null(identityAccessor.Identity);
        }

        [Fact]
        public void when_identity_is_valid()
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("test"));
            var mockIPrincipalAccessor = MockIPrincipalAccessor(claimsPrincipal);
            IIdentityAccessor identityAccessor = new IdentityAccessor(mockIPrincipalAccessor.Object);

            Assert.NotNull(identityAccessor.Identity);
            Assert.Equal("test", identityAccessor.Identity.AuthenticationType);
        }

        private Mock<IPrincipalAccessor> MockIPrincipalAccessor(ClaimsPrincipal claimsPrincipal)
        {
            var mockHttpMessageHandler = new Mock<IPrincipalAccessor>();

            mockHttpMessageHandler.Setup(_ => _.Principal).Returns(claimsPrincipal);

            return mockHttpMessageHandler;
        }
    }
}
