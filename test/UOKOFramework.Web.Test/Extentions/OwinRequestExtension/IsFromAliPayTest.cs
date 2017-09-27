using Microsoft.Owin;
using Xunit;
using UOKOFramework.Web.Extentions;

namespace UOKOFramework.Web.Test.Extentions.OwinRequestExtension
{
    public class IsFromAliPayTest : BaseTest
    {
        [Fact]
        public void when_this_is_null_shoule_return_false()
        {
            IOwinRequest owinRequest = null;

            Assert.False(owinRequest.IsFromAliPay());
        }

        [Theory]
        [InlineData("Alipay", true)]
        [InlineData("alipay", true)]
        [InlineData("abc", false)]
        [InlineData(null, false)]
        public void when_this_is_valid(string userAgent, bool result)
        {
            var owinRequest = BuildIOwinRequest("User-Agent", userAgent);
            Assert.Equal(result, owinRequest.IsFromAliPay());
        }
    }
}
