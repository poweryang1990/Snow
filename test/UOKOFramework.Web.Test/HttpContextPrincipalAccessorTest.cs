using System.IO;
using System.Security.Claims;
using System.Threading;
using System.Web;
using UOKOFramework.Security;
using Xunit;

namespace UOKOFramework.Web.Test
{
    public class GetCleintIP
    {
        [Fact]
        public void when_HttpContext_Current_is_null_should_return_ThreadCurrentPrincipal()
        {
            HttpContext.Current = null;
            Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity("thread"));

            IPrincipalAccessor principalAccessor = new HttpContextPrincipalAccessor();

            Assert.Equal("thread", principalAccessor.Principal.Identity.AuthenticationType);
        }

        [Fact]
        public void when_HttpContext_Current_is_not_null_should_return_HttpContext_Current_User()
        {
            var httpRequest = new HttpRequest("", "http://localhost", "");
            var httpResponse = new HttpResponse(new StringWriter());
            HttpContext.Current = new HttpContext(httpRequest, httpResponse)
            {
                User = new ClaimsPrincipal(new ClaimsIdentity("cookie"))
            };
            Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity("thread"));

            IPrincipalAccessor principalAccessor = new HttpContextPrincipalAccessor();

            Assert.Equal("cookie", principalAccessor.Principal.Identity.AuthenticationType);
        }
    }
}
