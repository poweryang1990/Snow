using Microsoft.Owin;
using Xunit;
using UOKOFramework.Web.Extensions;

namespace UOKOFramework.Web.Test.Extensions.OwinRequestExtension
{
    public class IsAjaxRequestTest : BaseTest
    {
        [Fact]
        public void when_this_is_null_shoule_return_false()
        {
            IOwinRequest owinRequest = null;

            Assert.False(owinRequest.IsAjaxRequest());
        }

        [Theory]
        [InlineData("XMLHttpRequest", true)]
        [InlineData("abc", false)]
        [InlineData(null, false)]
        public void when_this_is_valid(string queryValue, bool isAjaxRequest)
        {
            var owinRequest = BuildIOwinRequest(null, null);
            if (queryValue != null)
            {
                owinRequest.QueryString = new QueryString("X-Requested-With", queryValue);
            }

            Assert.Equal(isAjaxRequest, owinRequest.IsAjaxRequest());


            owinRequest = BuildIOwinRequest("X-Requested-With", queryValue);
            Assert.Equal(isAjaxRequest, owinRequest.IsAjaxRequest());
        }
    }
}
