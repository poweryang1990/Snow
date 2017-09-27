using Microsoft.Owin;
using Xunit;
using UOKOFramework.Web.Extentions;

namespace UOKOFramework.Web.Test.Extentions.OwinRequestExtension
{
    public class IsBearerAuthorizationTest : BaseTest
    {
        [Fact]
        public void when_this_is_null_shoule_return_false()
        {
            IOwinRequest owinRequest = null;

            Assert.False(owinRequest.IsBearerAuthorization());
        }

        [Theory]
        [InlineData("Bearer ", true)]
        [InlineData("bearer ", false)]
        [InlineData("Bearer", false)]
        [InlineData("abc", false)]
        [InlineData(null, false)]
        public void when_this_is_valid(string authorization, bool result)
        {
            var owinRequest = BuildIOwinRequest("Authorization", authorization);
            Assert.Equal(result, owinRequest.IsBearerAuthorization());
        }
    }
}
